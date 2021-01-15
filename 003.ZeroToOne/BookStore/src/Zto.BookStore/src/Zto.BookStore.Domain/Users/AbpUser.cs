using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Users;

namespace Zto.BookStore.Users
{
    /* This entity shares the same table/collection ("AbpUsers" by default) with the
     * IdentityUser entity of the Identity module.
     *
     * - You can define your custom properties into this class.
     * - You never create or delete this entity, because it is Identity module's job.
     * - You can query users from database with this entity.
     * - You can update values of your custom properties.
     * 
     * 这个实体属性与身份模块的IdentityUser共享相同的表/集合(默认为“AbpUsers”)
     * 
     * -你可以在这个类中定义你的自定义属性。
     * -永远不要创建或删除此实体，因为它是标识模块的工作。
     * -你可以用这个实体从数据库查询用户。
     * -您可以更新自定义属性的值。
     */
    public class AbpUser : FullAuditedAggregateRoot<Guid>, IUser
    {
        #region Base properties

        /* These properties are shared with the IdentityUser entity of the Identity module.
         * Do not change these properties through this class. Instead, use Identity module
         * services (like IdentityUserManager) to change them.
         * So, this properties are designed as read only!
         * 
         * 这些属性与标识模块的IdentityUser实体共享。
         *不要通过该类更改这些属性。相反，使用Identity模块
         *服务(如IdentityUserManager)更改它们。
         *因此，这些属性被设计为只读!
         */

    public virtual Guid? TenantId { get; private set; }

        public virtual string UserName { get; private set; }

        public virtual string Name { get; private set; }

        public virtual string Surname { get; private set; }

        public virtual string Email { get; private set; }

        public virtual bool EmailConfirmed { get; private set; }

        public virtual string PhoneNumber { get; private set; }

        public virtual bool PhoneNumberConfirmed { get; private set; }

        #endregion


        /* Add your own properties here. Example:
         *
         * public string MyProperty { get; set; }
         *
         * If you add a property and using the EF Core, remember these;
         *
         * 1. Update BookStoreDbContext.OnModelCreating
         * to configure the mapping for your new property
         * 2. Update BookStoreEfCoreEntityExtensionMappings to extend the IdentityUser entity
         * and add your new property to the migration.
         * 3. Use the Add-Migration to add a new database migration.
         * 4. Run the .DbMigrator project (or use the Update-Database command) to apply
         * schema change to the database.
         */

        private AppUser()
        {

        }
    }
}
