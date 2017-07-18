


using System.Data.Common;
using Abp.EntityFramework;
using System.Data.Entity;

namespace MyProject.EntityFramework
{
    public class MyProjectDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...

        //Example:
        //public virtual IDbSet<User> Users { get; set; }
		
		public virtual IDbSet<Sys_LSH> Sys_LSHs { get; set; } 
		
		public virtual IDbSet<Sys_MenuModule> Sys_MenuModules { get; set; } 
		
		public virtual IDbSet<Sys_User> Sys_Users { get; set; } 
		
		public virtual IDbSet<Sys_Dic> Sys_Dics { get; set; } 
		
		public virtual IDbSet<Sys_DicType> Sys_DicTypes { get; set; } 
		
        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public MyProjectDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in MyProjectDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of MyProjectDbContext since ABP automatically handles it.
         */
        public MyProjectDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public MyProjectDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public MyProjectDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}

