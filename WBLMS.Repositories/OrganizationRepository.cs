using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text;
using WBLMS.Data;
using WBLMS.IRepositories;
using WBLMS.Models;
using WBLMS.Models.QueryParameters;
using System.Linq.Dynamic.Core;

namespace WBLMS.Repositories
{
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        private readonly WBLMSDbContext _dbContext;
        public OrganizationRepository(WBLMSDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateOrganization(Organization organization)
        {
            try
            {
                if (organization == null)
                {
                    throw new ArgumentNullException(nameof(organization));
                }
                _dbContext.Add(organization);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Error While saving organization {ex.Message}");
                throw;
            }
        }

        public async Task<(PagedList<Organization>, PaginationMetadata)> GetAllOrganizations(string? search, OrganizationQueryParameters orgQParam)
        {
            try
            {
                var orgsWithBranches = _dbContext.Organizations
                        .Include(o => o.Branches).ThenInclude(o => o.Departments).AsQueryable().AsNoTracking();

                // Adding Filtering Logic here
                ApplyFilter(ref orgsWithBranches, orgQParam);
                // Sorting Logic
                ApplySort(ref orgsWithBranches, orgQParam.OrderBy);

                // Pagination Logic
                var pagedOrgList = PagedList<Organization>.ToPagedList(orgsWithBranches, orgQParam.PageNumber, orgQParam.PageSize);
                var metadata = new PaginationMetadata()
                {
                    TotalCount = pagedOrgList.TotalCount,
                    TotalPages = pagedOrgList.TotalPages,
                    CurrentPage = pagedOrgList.CurrentPage,
                    PageSize = pagedOrgList.PageSize,
                    HasNext = pagedOrgList.HasNext,
                    HasPrevious = pagedOrgList.HasPrevious
                };
                return (pagedOrgList, metadata);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Error Getting all ogranization {ex.Message}");
                throw;
            }
        }

        public async Task<bool> OrganizationExists(int organizationId)
        {
            return await _dbContext.Organizations.AnyAsync(o => o.Id == organizationId);
        }

        private void ApplySort(ref IQueryable<Organization> orgs, string orderByQueryString)
        {
            if(!orgs.Any()) return;
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                orgs = orgs.OrderBy(o => o.Name);
                return;
            }
            var orderParam = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Organization).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach ( var param in orderParam)
            {
                if (string.IsNullOrEmpty(param))
                {
                    continue;
                }
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if(objectProperty == null)
                {
                    continue;
                }

                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                orgs = orgs.OrderBy(x => x.Name);
                return;
            }
            orgs = orgs.OrderBy(orderQuery);
        }
        private void ApplyFilter(ref IQueryable<Organization> orgs, OrganizationQueryParameters orgParams)
        {
            if (!orgs.Any()) return;
            if (!string.IsNullOrWhiteSpace(orgParams.Name))
            {
                orgs = orgs.Where(o => o.Name.ToLower().Contains(orgParams.Name.ToLower()));
            }

        }
    }
}
