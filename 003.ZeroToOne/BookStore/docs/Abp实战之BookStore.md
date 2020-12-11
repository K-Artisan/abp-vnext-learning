[TOC]

# 前言

本系列参照官方文档[BookStore](https://docs.abp.io/zh-Hans/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF),创建一个BookStore应用程序。

旨在**从零开始(ZeroToOne, zto)**不使用模板，

从创建一个空的解决方案开始，一步一步地去了解如何使用`Abp.vNext`去构建一个应用程序。



# 1.初步构建项目结构

创建一个空的解决方案`Zto.BookStore`,然后依次添加如下项目，

注意：以下创建的项目，都的以`Zto.BookStore`.为前缀，为了叙述的简单，故省略之，比如：

`*.Domain`指的是项目`Zto.BookStore.Domain`



[模块化架构最佳实践 & 约定](https://docs.abp.io/zh-Hans/abp/latest/Best-Practices/Module-Architecture)

[应用程序启动模板](https://docs.abp.io/zh-Hans/abp/latest/Startup-Templates/Application)



## 项目结构

<img src="images/Abp%E5%AE%9E%E6%88%98%E4%B9%8BBookStore/layered-project-dependencies.png" alt="layered-project-dependencies" style="zoom: %;" />



## 1.1 *.Domain.Shared 项目

创建一个.NetCore类库项目

### 基本设置

- 修改默认命名空间为`Zto.BookStore`

- 新建文件`Localization`



### 依赖包

- `Volo.Abp.Core`



### **知识点**: Abp模块化

参考资料：

- [Abp-Notes:模块化](https://www.cnblogs.com/easy5weikai/p/13959787.html)

  

### 创建`AbpModule`

根目录下创建`AbpModule`:

```C#
using Volo.Abp.Modularity;

namespace Zto.BookStore
{
    public class BookStoreDomainSharedModule : AbpModule
    {
    }
}
```



### 创建`BookType`

创建文件夹`Books`,在该文件夹下新建`BookType.cs`:

```C#
namespace Zto.BookStore.Books
{
    public enum BookType
    {
        Undefined, //未定义的
        Adventure, //冒险
        Biography, //传记
        Dystopia,  //地狱
        Fantastic, //神奇的
        Horror,    //恐怖,
        Science,   //科学
        ScienceFiction, //科幻小说
        Poetry     //诗歌
    }
}

```



### Book相关常量

在`Books`文件夹下新建一个`BookConsts.cs`类，用于存储`Book`相关常量值

```C#
namespace Zto.BookStore.Books
{
    public static class BookConsts
    {
        public const int MaxNameLength = 256; //名字最大长度
    }
}
```



### 本地化

[官方文档](https://docs.abp.io/zh-Hans/abp/latest/Localization)

#### 创建本地化资源

开始的UI开发之前,我们首先要准备本地化的文本(这是通常在开发应用程序时需要做的).

本地化资源用于将相关的本地化字符串组合在一起,并将它们与应用程序的其他本地化字符串分开，

通常一个模块会定义自己的本地化资源. 本地化资源就是一个普通的类. 例如:

- 在文件夹`Localization`下，新建`BookStoreResource.cs`类

```C#
    [LocalizationResourceName("BookStore")]
    public class BookStoreResource
    {

    }
```

` [LocalizationResourceName("BookStore")]`标记资源名



- 在文件夹`Localization/BookStore`，添加两个语言资源`json`文件，

  - en.json

    ```json
    {
      "Culture": "en",
      "Texts": {
        "Menu:Home": "Home",
        "Welcome": "Welcome",
        "LongWelcomeMessage": "Welcome to the application. This is a startup project based on the ABP framework. For more information, visit abp.io.",
        "Menu:BookStore": "Book Store",
        "Menu:Books": "Books",
        "Actions": "Actions",
        "Edit": "Edit",
        "PublishDate": "Publish date",
        "NewBook": "New book",
        "Name": "Name",
        "Type": "Type",
        "Price": "Price",
        "CreationTime": "Creation time",
        "AreYouSureToDelete": "Are you sure you want to delete this item?",
        "Enum:BookType:0": "Undefined",
        "Enum:BookType:1": "Adventure",
        "Enum:BookType:2": "Biography",
        "Enum:BookType:3": "Dystopia",
        "Enum:BookType:4": "Fantastic",
        "Enum:BookType:5": "Horror",
        "Enum:BookType:6": "Science",
        "Enum:BookType:7": "Science fiction",
        "Enum:BookType:8": "Poetry",
        "BookDeletionConfirmationMessage": "Are you sure to delete the book '{0}'?",
        "SuccessfullyDeleted": "Successfully deleted!",
        "Permission:BookStore": "Book Store",
        "Permission:Books": "Book Management",
        "Permission:Books.Create": "Creating new books",
        "Permission:Books.Edit": "Editing the books",
        "Permission:Books.Delete": "Deleting the books",
        "BookStore:00001": "There is already an author with the same name: {name}",
        "Permission:Authors": "Author Management",
        "Permission:Authors.Create": "Creating new authors",
        "Permission:Authors.Edit": "Editing the authors",
        "Permission:Authors.Delete": "Deleting the authors",
        "Menu:Authors": "Authors",
        "Authors": "Authors",
        "AuthorDeletionConfirmationMessage": "Are you sure to delete the author '{0}'?",
        "BirthDate": "Birth date",
        "NewAuthor": "New author"
      }
    }
    
    ```

    

  - zh-Hans.json

    ```json
    {
      "culture": "zh-Hans",
      "texts": {
        "Menu:Home": "首页",
        "Welcome": "欢迎",
        "LongWelcomeMessage": "欢迎来到该应用程序. 这是一个基于ABP框架的启动项目. 有关更多信息, 请访问 abp.io.",
    
        "Enum:BookType:0": "未知",
        "Enum:BookType:1": "冒险",
        "Enum:BookType:2": "传记",
        "Enum:BookType:3": "地狱",
        "Enum:BookType:4": "神奇的",
        "Enum:BookType:5": "恐怖",
        "Enum:BookType:6": "科学",
        "Enum:BookType:7": "科幻小说 ",
        "Enum:BookType:8": "诗歌"
      }
    }
    ```

    - 每个本地化文件都需要定义 `culture` (文化) 代码 (例如 "en" 或 "en-US").

    - `texts` 部分只包含本地化字符串的键值集合 (键也可能有空格).

      

**特别注意**：

必须将语言资源文件的属性设置为

1.  复制到输出目录：不复制
2. 生成操作：嵌入的资源

####              设置本地化资源

```C#
    public class BookStoreDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {

        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BookStoreDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<BookStoreResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/BookStore");

                options.DefaultResourceType = typeof(BookStoreResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("BookStore", typeof(BookStoreResource));
            });
        }
    }
```

在`ConfigureServices `方法中设置本地化资源



## 1.2 *.Domain 项目

创建一个.NetCore类库项目

### 基本设置

- 修改默认命名空间为`Zto.BookStore`

### 项目引用

- `*.Domain.Shared`

  

### 依赖包

- `Volo.Abp.Core`



### 创建`AbpModule`

根目录下创建`AbpModule`:

```C#
using Volo.Abp.Modularity;

namespace Zto.BookStore
{
    [DependsOn(typeof(BookStoreDomainSharedModule))]
    public class BookStoreDomainModule : AbpModule
    {
    }
}
```



### 创建`Book`领域模型

创建文件夹`Books`,在该文件夹下新建`Book.cs`

```C#
using Volo.Abp.Domain.Entities.Auditing;
using System;

namespace Zto.BookStore.Books
{
    public class Book : AuditedAggregateRoot<Guid>
    {
        public Guid AuthorId { get; set; }
        public String Name { get; set; }
        public BookType Type { get; set; }
        public DateTime PublishDate { get; set; }
        public float Price { get; set; }
    }
}
```



### 项目常量值类`BookStoreConsts`

在根目录下创建`BookStoreConsts.cs`,用于保存项目中常量数据值

```C#
namespace Zto.BookStore
{
    public static class BookStoreConsts
    {
        public const string DbTablePrefix = "Bks"; //常量值:表前缀
        public const string DbSchema = null; //常量值:表的架构
    }
}
```



## 1.3 *.EntityFrameworkCore 项目

创建一个.NetCore类库项目

### 基本设置

- 修改默认命名空间为`Zto.BookStore`
- 创建文件夹`EntityFrameworkCore`

### 项目引用

- `*.Domain`

  

### 依赖包

- `Volo.Abp.EntityFrameworkCore`

- `Volo.Abp.EntityFrameworkCore.SqlServer`:使用MsSqlServer数据库



### 创建`AbpModule`

在文件夹`EntityFrameworkCore`下创建`AbpModule`:

```C#
using Volo.Abp.Modularity;

namespace Zto.BookStore.EntityFrameworkCore
{
    [DependsOn(typeof(BookStoreDomainModule))]
    public class BookStoreEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<BookStoreDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also BookStoreMigrationsDbContextFactory for EF Core tooling. */
                options.UseSqlServer();
            });
        }
    }
}
```

代码解析：

- `AddDefaultRepositories(includeAllEntities: true)`

  添加默认`Repository`实现，`includeAllEntities: true`表示为所以实体类实现仓储(`Repository`)类

  

- `options.UseSqlServer();`使用MsSqlServer数据库



### 创建`DbContext`

在文件夹`EntityFrameworkCore`中创建`BookStoreDbContext.cs`

```C#
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Zto.BookStore.Books;

namespace Zto.BookStore.EntityFrameworkCore
{
    [ConnectionStringName("BookStoreConnString")]
    public class BookStoreDbContext : AbpDbContext<BookStoreDbContext>
    {
        public DbSet<Book> Books { get; set; }
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            /* Configure the shared tables (with included modules) here */
            // 配置从其它modules引入的模型


            /* Configure your own tables/entities inside the ConfigureBookStore method */
            // 配置本项目自己的表和实体模型
            builder.ConfigureBookStore();
        }
    }
}
```

代码解析：

- `[ConnectionStringName("BookStoreConnString")]`：表示要使用的数据库连接字符串



### BookStore的EFcore 实体模型映射

创建`/EntityFrameworkCore/BookStoreDbContextModelCreatingExtensions.cs`:

该类用于配置本项目（即：BookStore项目）自己的表和实体模型

```C#
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Zto.BookStore.Books;

namespace Zto.BookStore.EntityFrameworkCore
{
    public static class BookStoreDbContextModelCreatingExtensions
    {
        public static void ConfigureBookStore(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */
            builder.Entity<Book>(e =>
            {
                e.ToTable(BookStoreConsts.DbTablePrefix + "Books", BookStoreConsts.DbSchema);
                e.ConfigureByConvention(); //auto configure for the base class props ,优雅的配置和映射继承的属性,应始终对你所有的实体使用它.
                e.Property(p => p.Name).HasMaxLength(BookConsts.MaxNameLength);

            });
        }
    }
}
```

其中：

-  `e.ToTable(BookStoreConsts.DbTablePrefix + "Books", BookStoreConsts.DbSchema);`

  配置表的前缀和表的架构
  
-   `e.ConfigureByConvention();`优雅的配置和映射继承的属性,应始终对你所有的实体使用它

  

### 命令行中执行数据库迁移

如果严格按上述顺序依次创建项目，并添加代码

这时，我们可以随便创建一个控制台程序，并添加配置文件`appsettings.json`

```json
{
  "ConnectionStrings": {
    "BookStoreConnString": "Server=.;Database=BookStore_Zto;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

1. 设置控制台程序为默认启动项目，

2. 打开程【序包管理器控制台】，并将【默认项目】设置为项目：`.EntityFrameworkCore.DbMigrations` , 

3. 执行EF数据库迁移命令

```powershell
add-migration initDb
```

会抛出如下错误：

```powershell
Unable to create an object of type 'BookStoreDbContext'. For the different patterns supported at design time, see https://go.microsoft.com/fwlink/?linkid=851728
```

这是因为：我们没有为`BookStoreDbContext`提供无参数构造函数，但是``BookStoreDbContext`必须得继承` AbpDbContext<BookStoreDbContext>`,其不提供无参数构造函数，故在项目`*.EntityFrameworkCore.DbMigrations`中是无法执行数据库迁移的，如何解决数据库迁移呢？请看章节【**设计时创建`DbContext`**】。



## 1.4 *.EntityFrameworkCore.DbMigrations 项目

- **Q1:**为什么要创建这个工程呢?

​       **A: **用于EF的数据库迁移，因为如果项目是使用其它的 O/R框架 ，迁移的方式就不一样，所以数据库的迁移，也使用接口方式，这样就可以替换。



### 基本设置

- 修改默认命名空间为`Zto.BookStore`

- 创建文件夹`EntityFrameworkCore`

### 项目引用

- `*.EntityFrameworkCore`



### 依赖包

- `Microsoft.EntityFrameworkCore.Design`:设计时创建`DbContex`,用于命令行执行数据库迁移

### 创建`AbpModule`

在文件夹`EntityFrameworkCore`下创建`AbpModule`:

```C#
using Volo.Abp.Modularity;

namespace Zto.BookStore.EntityFrameworkCore
{
    [DependsOn(
        typeof(BookStoreEntityFrameworkCoreModule)
        )]
    public class BookStoreEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        context.Services.AddAbpDbContext<BookStoreMigrationsDbContext>();
    }
}
```



### 迁移`DbContexnt`

在文件夹`EntityFrameworkCore`下创建`BookStoreMigrationsDbContext.cs`，

此`DbContext`仅仅用于数据库迁移，故：

- 它仅仅用于数据库迁移，运行时使用的还是`BookStoreDbContext`

- `DbSet<>`将不用加了

  ```C#
   public DbSet<Book> Books { get; set; }
  ```

  这样的`DbSet<>`代码就不用添加了。



`BookStoreMigrationsDbContext.cs`代码如下：

```C#
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Zto.BookStore.EntityFrameworkCore
{
    /// <summary>
    /// This DbContext is only used for database migrations.
    /// It is not used on runtime. See BookStoreDbContext for the runtime DbContext.
    /// It is a unified model that includes configuration for
    /// all used modules and your application.
    /// 
    /// 这个DbContext只用于数据库迁移。
    /// 它不在运行时使用。有关运行时DbContext，请参阅BookStoreDbContext。
    /// 它是一个统一配置所有使用的模块和您的应用程序的模型
    /// </summary>
    [ConnectionStringName("BookStoreConnString")]
    public class BookStoreMigrationsDbContext : AbpDbContext<BookStoreMigrationsDbContext>
    {
        public BookStoreMigrationsDbContext(DbContextOptions<BookStoreMigrationsDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */
            // 配置从其它modules引入的模型



            /* Configure your own tables/entities inside the ConfigureBookStore method */
            // 配置本项目自己的表和实体模型
            builder.ConfigureBookStore();
        }

    }
}

```

**注意：在此处我们就通过特性`[ConnectionStringName("BookStoreConnString")]`指定其连接字符串**



### 设计时创建`DbContext`

在章节【 *.EntityFrameworkCore -- >  命令行中执行数据库迁移】中，看到那时使用ef命令是执行数据库迁移的时，会抛出如下异常：

```pow
Unable to create an object of type 'BookStoreDbContext'. For the different patterns supported at design time, see https://go.microsoft.com/fwlink/?linkid=851728
```

解决方案就是**设计时创建`DbContext`**。



#### 什么是设计时创建`DbContext`

参考资料：
https://docs.microsoft.com/zh-cn/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli

> 从设计时工厂创建`DbContext`：
> 你还可以通过实现接口来告诉工具如何创建 `DbContext IDesignTimeDbContextFactory<TContext> `：
> 如果实现此接口的类在与派生的项目相同的项目中` DbContext` 
> 或在应用程序的启动项目中找到，
> 则这些工具将绕过创建 `DbContext` 的其他方法，并改用设计时工厂。
>
> 如果需要以不同于运行时的方式配置 `DbContext` 的设计时，则设计时工厂特别有用 `DbContext` 。如果构造函数采用其他参数，
> 但未在 di 中注册，如果根本不使用 di，
> 或者出于某种原因而不是使用 `CreateHostBuilder` ASP.NET Core 应用程序的类中的方法 `Main `。

**总之一句话：**
**实现了`IDesignTimeDbContextFactory<BookStoreMigrationsDbContext>`，**
**就可以使用命令行执行数据库迁移**，例如：

- 在 NET Core CLI中执行： dotnet ef database update

- 在 Visual Studio中执行：Update-Database 

  

#### 实现`IDesignTimeDbContextFactory<>`

综上，

1. 确保已入如下`Nuget`包：

   - `Microsoft.EntityFrameworkCore.Design`

   - `Volo.Abp.EntityFrameworkCore.SqlServer`

     如果使用的是MySql数据库，引入的包是`Volo.Abp.EntityFrameworkCore.MySQL`
     
     

2. 在文件夹`EntityFrameworkCore`下创建`BookStoreMigrationsDbContextFactory`,

```c#
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Zto.BookStore.EntityFrameworkCore
{
    /// <summary>
    ///   This class is needed for EF Core console commands
    ///   (like Add-Migration and Update-Database commands) 
    ///   
    ///   参考资料：
    ///   https://docs.microsoft.com/zh-cn/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
    ///   从设计时工厂创建DbContext：
    ///   你还可以通过实现接口来告诉工具如何创建 DbContext IDesignTimeDbContextFactory<TContext> ：
    ///   如果实现此接口的类在与派生的项目相同的项目中 DbContext 
    ///   或在应用程序的启动项目中找到，
    ///   则这些工具将绕过创建 DbContext 的其他方法，并改用设计时工厂。
    /// 
    ///   如果需要以不同于运行时的方式配置 DbContext 的设计时，则设计时工厂特别有用 DbContext 。如果构造函数采用其他参数，
    ///   但未在 di 中注册，如果根本不使用 di，
    ///   或者出于某种原因而不是使用 CreateHostBuilder ASP.NET Core 应用程序的类中的方法 Main 。
    /// 
    /// 
    ///   总之一句话：
    ///   实现了IDesignTimeDbContextFactory<BookStoreMigrationsDbContext>，
    ///   就可以使用命令行执行数据库迁移,
    ///      (1).在 NET Core CLI中执行： dotnet ef database update
    ///      (2).在 Visual Studio中执行：Update-Database 
    /// </summary>
    public class BookStoreMigrationsDbContextFactory : IDesignTimeDbContextFactory<BookStoreMigrationsDbContext>
    {
        public BookStoreMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();
            var builder = new DbContextOptionsBuilder<BookStoreMigrationsDbContext>()
                 .UseSqlServer(configuration.GetConnectionString("BookStoreConnString")); //SqlServer数据库
                //.UseMySql(configuration.GetConnectionString("BookStoreConnString"), ServerVersion.); //MySql数据库

            return new BookStoreMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                //项目Zto.BookStore.DbMigrator的根目录
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Zto.BookStore.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();

            return builder.Build();
        }
    }
}
```

这样就可以在**NET Core CLI**或**Visual Studio**中使用诸如如下命令执行数据库迁移

```md
//vs中使用
Add-Migration

//or NET Core CLI 中使用
dotnet ef database update
```

ef命名会自动找到类`BookStoreMigrationsDbContextFactory`

```c#
public class BookStoreMigrationsDbContextFactory : IDesignTimeDbContextFactory<BookStoreMigrationsDbContext>
```

这时，我们可以随便创建一个控制台程序（本例为项目`Zto.BookStore.DbMigrator`），并添加配置文件`appsettings.json`

```json
{
  "ConnectionStrings": {
    "BookStoreConnString": "Server=.;Database=BookStore_Zto;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

1. 设置控制台程序为默认启动项目，

   不过，如果现在已经通过以下代码在`BookStoreMigrationsDbContextFactory`中明确指明了配置文件的地址：

   ```C#
           private static IConfigurationRoot BuildConfiguration()
           {
               var builder = new ConfigurationBuilder()
                   .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Zto.BookStore.DbMigrator/"))
                   .AddJsonFile("appsettings.json", optional: false);
   
               return builder.Build();
           }
   ```

   即，如下代码

   ```C#
     .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Zto.BookStore.DbMigrator/"))
   ```

   **指明了配置文件位于项目`Zto.BookStore.DbMigrator`的根目中，所以这时可以不用将设置控制台程序为默认启动项目**

   

2. 打开程【程序包管理器控制台】，并将【默认项目】设置为项目：*`.EntityFrameworkCore.DbMigrations` , 

3. 执行EF数据库迁移命令

   ```powershell
   add-migration initDb
   ```

   这时，命令行提示：

   ```powershell
   PM> add-migration initDb
   Build started...
   Build succeeded.
   To undo this action, use Remove-Migration.
   ```

4. 把挂起的`migration`更新到数据库

   ```powershell
   update-database
   ```

   这时，命令行提示：

   ```powershell
   PM> update-database
   Build started...
   Build succeeded.
   Security Warning: The negotiated TLS 1.0 is an insecure protocol and is supported for backward compatibility only. The recommended protocol version is TLS 1.2 and later.
   Security Warning: The negotiated TLS 1.0 is an insecure protocol and is supported for backward compatibility only. The recommended protocol version is TLS 1.2 and later.
   Security Warning: The negotiated TLS 1.0 is an insecure protocol and is supported for backward compatibility only. The recommended protocol version is TLS 1.2 and later.
   Applying migration '20201207183001_initDb'.
   Done.
   PM> 
   ```

   同时在项目`.EntityFrameworkCore.DbMigrations`的根目录下,会自动生成文件夹`Migrations`,其中包含两个文件

   - `20201207183001_initDb.cs`

     ```C#
     using System;
     using Microsoft.EntityFrameworkCore.Migrations;
     
     namespace Zto.BookStore.Migrations
     {
         public partial class initDb : Migration
         {
             protected override void Up(MigrationBuilder migrationBuilder)
             {
                 migrationBuilder.CreateTable(
                     name: "BksBooks",
                     columns: table => new
                     {
                         Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                         AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                         Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                         Type = table.Column<int>(type: "int", nullable: false),
                         PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                         Price = table.Column<float>(type: "real", nullable: false),
                         ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                         ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                         CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                         CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                         LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                         LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                     },
                     constraints: table =>
                     {
                         table.PrimaryKey("PK_BksBooks", x => x.Id);
                     });
             }
     
             protected override void Down(MigrationBuilder migrationBuilder)
             {
                 migrationBuilder.DropTable(
                     name: "BksBooks");
             }
         }
     }
     
     ```

   - `BookStoreMigrationsDbContextModelSnapshot.cs`：迁移快照

   

5. 数据库也自动生成了数据库及其相关表

   <img src="images/Abp%E5%AE%9E%E6%88%98%E4%B9%8BBookStore/image-20201208191539533.png" alt="image-20201208191539533" style="zoom:80%;" />

 



### 在项目`*.EntityFrameworkCore.DbMigrations`中数据库迁移的局限性

直接在项目`*.EntityFrameworkCore.DbMigrations`中使用命令行执行数据库迁移有如下局限性：

- 不能支持多租户(如果开发的系统要求支持多租户的话)的数据库迁移

- 不能执行种子数据：

  使用EF Core执行标准的 `Update-Database` 命令,但是它不会初始化种子数据.



鉴于以上局限性，我们把数据库迁移的工作全部集中到**控制台项目`.DbMigrator`**中，以下两节所创建的类

- `EntityFrameworkCoreBookStoreDbSchemaMigrator`

- `BookStoreDbMigrationService`

就是为了这个目标而提前准备的。



### 迁移接口：IBookStoreDbSchemaMigrator

在**项目`*.Domain`的`/Data`**文件夹下，创建接口：`IBookStoreDbSchemaMigrator`，如下所示：

```C#
public interface IBookStoreDbSchemaMigrator
{
    Task MigrateAsync();
}
```
创建其实现类`EntityFrameworkCoreBookStoreDbSchemaMigrator`，主要是通过代码

```C#
dbContext.database.MigrateAsync();
```

更新`migration`到数据库：

```C#
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Zto.BookStore.Data;

namespace Zto.BookStore.EntityFrameworkCore
{
    public class EntityFrameworkCoreBookStoreDbSchemaMigrator : IBookStoreDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreBookStoreDbSchemaMigrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrationAsync()
        {
            /*
            * 我们有意从IServiceProvider解析BookStoreMigrationsDbContext(而不是直接注入它)，
            * 是为了能正确获取当前的范围、当前租户的连接字符串
            */
            var dbContext = _serviceProvider.GetRequiredService<BookStoreMigrationsDbContext>();
            var database = dbContext.Database;
            //var connString = database.GetConnectionString();

            /*
             * Asynchronously applies any pending migrations for the context to the database.
             * Will create the database if it does not already exist.
             */
            await database.MigrateAsync();
        }
    }
}
```

**特别注意：**

​	  **database.MigrateAsync();只是相当于`update-database`，故：在该方法执行前，**

**确保已经手动执行命令`add-migration xxx`创建`migration`**



### 数据库迁移服务

创建一个数据库迁移服务`BookStoreDbMigrationService`,使用代码（而不是EFCore命令行）统一管理所有数据库迁移任务，比如：

- 调用实现了上节所定义的接口`IBookStoreDbSchemaMigrator`的实现类，
- 若系统执行多租户，为租户执行数据库迁移
- 执行种子数




其中，关键性代码如下：

- 更新`migration`到数据库

  ```C#
  await database.MigrateAsync();
  ```

- 执行种子数据

  ```C#
   _dataSeeder.SeedAsync(tenant?.Id);
  ```

完整代码如下：

`BookStoreDbMigrationService.cs`

```C#
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;

namespace Zto.BookStore.Data
{
    public class BookStoreDbMigrationService : ITransientDependency
    {
        public ILogger<BookStoreDbMigrationService> Logger { get; set; }

        private readonly IDataSeeder _dataSeeder;
        private readonly IEnumerable<IBookStoreDbSchemaMigrator> _dbSchemaMigrators;
        private readonly ITenantRepository _tenantRepository;
        private readonly ICurrentTenant _currentTenant;

        public BookStoreDbMigrationService(
            IDataSeeder dataSeeder,
            IEnumerable<IBookStoreDbSchemaMigrator> dbSchemaMigrators,
            ITenantRepository tenantRepository,
            ICurrentTenant currentTenant)
        {
            _dataSeeder = dataSeeder;
            _dbSchemaMigrators = dbSchemaMigrators;
            _tenantRepository = tenantRepository;
            _currentTenant = currentTenant;

            Logger = NullLogger<BookStoreDbMigrationService>.Instance;
        }

        public async Task MigrateAsync()
        {
            Logger.LogInformation("Started database migrations...");

            await MigrateDatabaseSchemaAsync(); //执行数据库迁移
            await SeedDataAsync();  //执行种子数据
            Logger.LogInformation($"Successfully completed host database migrations.");

            /*-----------------------------------------------------------------
             * 以下为多租户执行的数据库迁移
             -----------------------------------------------------------------*/
            var tenants = await _tenantRepository.GetListAsync(includeDetails: true);
            var migratedDatabaseSchemas = new HashSet<string>();
            foreach (var tenant in tenants)
            {
                if (!tenant.ConnectionStrings.Any())
                {
                    continue;
                }

                using (_currentTenant.Change(tenant.Id))
                {
                    var tenantConnectionStrings = tenant.ConnectionStrings
                        .Select(x => x.Value)
                        .ToList();

                    if (!migratedDatabaseSchemas.IsSupersetOf(tenantConnectionStrings))
                    {
                        await MigrateDatabaseSchemaAsync(tenant);

                        migratedDatabaseSchemas.AddIfNotContains(tenantConnectionStrings);
                    }

                    await SeedDataAsync(tenant);
                }

                Logger.LogInformation($"Successfully completed {tenant.Name} tenant database migrations.");
            }

            Logger.LogInformation("Successfully completed database migrations.");
        }

        /// <summary>
        /// 执行数据库迁移
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        private async Task MigrateDatabaseSchemaAsync(Tenant tenant = null)
        {
            Logger.LogInformation(
                $"Migrating schema for {(tenant == null ? "host" : tenant.Name + " tenant")} database...");

            foreach (var migrator in _dbSchemaMigrators)
            {
                await migrator.MigrateAsync();
            }
        }

        /// <summary>
        /// 执行种子数据
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        private async Task SeedDataAsync(Tenant tenant = null)
        {
            Logger.LogInformation($"Executing {(tenant == null ? "host" : tenant.Name + " tenant")} database seed...");

            await _dataSeeder.SeedAsync(tenant?.Id);
        }
    }
}

```

代码解析：

- `MigrateDatabaseSchemaAsync()`循环执行所有数据库迁移接口实例
- `SeedDataAsync()`执行种子数据

- **`MigrateAsync()`方法将被下一节的创建的迁移控制台程序项目`.DbMigrator`使用，用于统一执行数据库迁移操作**

**注意**：

因为这里我们使用到了多租户数据库迁移的判定，需要额外已入以下包：

- `Volo.Abp.TenantManagement.Domain`



#### 简化`BookStoreDbMigrationService`

由于目前缺乏对

- 多租户
- [`IDataSeeder`](https://docs.abp.io/zh-Hans/abp/latest/Data-Seeding)

的了解，所以把跟它们相关的功能代码注释掉，简化后的``BookStoreDbMigrationService`如下:

```C#
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;

namespace Zto.BookStore.Data
{
    public class BookStoreDbMigrationService : ITransientDependency
    {
        public ILogger<BookStoreDbMigrationService> Logger { get; set; }
        
        private readonly IEnumerable<IBookStoreDbSchemaMigrator> _dbSchemaMigrators;

        public BookStoreDbMigrationService(
            IEnumerable<IBookStoreDbSchemaMigrator> dbSchemaMigrators)
        {
            _dbSchemaMigrators = dbSchemaMigrators;
            Logger = NullLogger<BookStoreDbMigrationService>.Instance;
        }

        public async Task MigrateAsync()
        {
            Logger.LogInformation("Started database migrations...");
            await MigrateDatabaseSchemaAsync(); //执行数据库迁移
            Logger.LogInformation("Successfully completed database migrations.");
        }

        /// <summary>
        /// 执行数据库迁移
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        private async Task MigrateDatabaseSchemaAsync(Tenant tenant = null)
        {
            Logger.LogInformation(
                $"Migrating schema for {(tenant == null ? "host" : tenant.Name + " tenant")} database...");

            foreach (var migrator in _dbSchemaMigrators)
            {
                await migrator.MigrateAsync();
            }
        }

    }
}

```



## 1.5 *.DbMigrator 项目

新建.Net Core控制台项目`*.DbMigrator`，以后**所有的数据库迁移都推荐使这个控制台项目进行**，

可以在**开发**和**生产**环境**迁移数据库架构**和**初始化种子数据**.

### 基本设置

- 创建配置文件`appsettings.json`:

  ```C#
  {
    "ConnectionStrings": {
      "BookStoreConnString": "Server=.;Database=BookStore_Zto;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
  }
  ```

**特别注意**：

 一定要把配置文件的属性设置为：

- 复制到输出目录：始终复制
- 生成操作：内容   

​              

### 项目引用

- `*.EntityFrameworkCore.DbMigrations`



### 依赖包

- `Microsoft.EntityFrameworkCore.Tools`：数据库迁移
- `Volo.Abp.Autofac`：依赖注入
- `Serilog`日志：
  - `Serilog.Sinks.File`
  - `Serilog.Sinks.Console`
  - `Serilog.Extensions.Logging`
- `Microsoft.Extensions.Hosting`:控制台宿主程序



### 创建`AbpModule`

在根目录下创建`AbpModule`:

```C#
using Volo.Abp.Autofac;
using Zto.BookStore.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Zto.BookStore.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(BookStoreEntityFrameworkCoreDbMigrationsModule)
        )]
    public class BookStoreDbMigratorModule : AbpModule
    {
    }
}
```



### 创建`HostServer`

#### 知识点：IHostedService

- [官方文档](https://docs.microsoft.com/zh-cn/dotnet/architecture/microservices/multi-container-microservice-net-applications/background-tasks-with-ihostedservice)
- [Artech](https://www.cnblogs.com/artech/tag/IHostedService/)
- [简单明了的示例](https://blog.csdn.net/qq_16587307/article/details/106335047)

> 当注册 `IHostedService` 时，.NET Core 会在应用程序启动和停止期间分别调用 `IHostedService` 类型的 `StartAsync()` 和 `StopAsync()` 方法。
>
> 此外，如果我们想控制我们自己的服务程序的生命周期，那么可以使用`IHostApplicationLifetime`



`IHostSerice`定义如下：

```C#

namespace Microsoft.Extensions.Hosting
{
    //
    // 摘要:
    //     Defines methods for objects that are managed by the host.
    public interface IHostedService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
```



#### 数据库迁移`HostedService`

创建一个名为`DbMigratorHostedService`的类，继承`IHostedService`接口

```C#
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Zto.BookStore.Data;

namespace Zto.BookStore.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        //自己控制的服务程序的生命周期
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = AbpApplicationFactory.Create<BookStoreDbMigratorModule>(options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            }))
            {
                application.Initialize();

                await application
                    .ServiceProvider
                    .GetRequiredService<BookStoreDbMigrationService>()
                    .MigrateAsync();

                application.Shutdown();

                _hostApplicationLifetime.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

```

其中，核心代码只是：

```C#
BookStoreDbMigrationService.MigrateAsync()
```

执行数据库的迁移，包括：更新`migration`和种子数据



### 依赖注入`HostedService`

#### 知识点：Serilog

[在控制台项目中使用Serilog](https://www.cnblogs.com/qtiger/p/13224015.html)



```C#
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.IO;
using System.Threading.Tasks;

namespace Zto.BookStore.DbMigrator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information() //设置最低等级
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) //根据命名空间或类型重置日志最小级别
                .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
                .MinimumLevel.Override("Zto.BookStore", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("Zto.BookStore", LogEventLevel.Information)
#endif
                .Enrich.FromLogContext()
                .WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(), "Logs/logs.txt")) //将日志写到文件
                .WriteTo.Console()//将日志写到控制台
                .CreateLogger();

            await CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) => logging.ClearProviders()) //Removes all logger providers from builder.
                .ConfigureServices((hostContext, services) =>
        {
            services.AddHostedService<DbMigratorHostedService>();
        });
    }
}

```

代码解析：

​       依赖注入`DbMigratorHostedService`服务，控制台程序自动将执行`HostService`的`StartAsync()`方法



### 执行数据库迁移

设置控制台程序为启动项目，并运行，执行数据库迁移。

控制台输出日志：

```powershell
[13:54:12 INF] Started database migrations...
[13:54:12 INF] Migrating schema for host database...
Security Warning: The negotiated TLS 1.0 is an insecure protocol and is supported for backward compatibility only. The recommended protocol version is TLS 1.2 and later.
[13:54:14 INF] Successfully completed host database migrations.
```

执行完成后，自动生成数据库及其相关表：

<img src="images/Abp%E5%AE%9E%E6%88%98%E4%B9%8BBookStore/image-20201208191539533.png" alt="image-20201208191539533" style="zoom:80%;" />





**特别注意：**

​	  这个控制台程序最终的本质是执行**`dbContext.database.MigrateAsync();`只是相当于`update-database`，**

**故：在该方法执行前，确保在项目`*.EntityFrameworkCore.DbMigrations`中已经手动执行命令`add-migration xxx`创建`migration`**



### 种子数据

> 在运行应用程序之前最好将初始数据添加到数据库中. 本节介绍ABP框架的[数据种子系统](https://docs.abp.io/zh-Hans/abp/latest/Data-Seeding). 如果你不想创建种子数据可以跳过本节,但是建议你遵循它来学习这个有用的ABP Framework功能。

#### `IDataSeedContributor`:种子数贡献者

在 `*.Domain` 项目下创建派生 `IDataSeedContributor` 的类,并且拷贝以下代码:

```C#
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Zto.BookStore.Books;

namespace Zto.BookStore
{
    public class BookStoreDataSeederContributor
      : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;

        public BookStoreDataSeederContributor(IRepository<Book, Guid> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookRepository.GetCountAsync() <= 0)
            {
                await _bookRepository.InsertAsync(
                    new Book
                    {
                        Name = "1984",
                        Type = BookType.Dystopia,
                        PublishDate = new DateTime(1949, 6, 8),
                        Price = 19.84f
                    },
                    autoSave: true
                );

                await _bookRepository.InsertAsync(
                    new Book
                    {
                        Name = "The Hitchhiker's Guide to the Galaxy",
                        Type = BookType.ScienceFiction,
                        PublishDate = new DateTime(1995, 9, 27),
                        Price = 42.0f
                    },
                    autoSave: true
                );
            }
        }
    }
}

```



如果数据库中当前没有图书,则此代码使用 `IRepository<Book, Guid>`(默认为[repository](https://docs.abp.io/zh-Hans/abp/latest/Repositories))将两本书插入数据库

其中，`IDataSeedContributor`接口如下:

```c#
namespace Volo.Abp.Data
{
    public interface IDataSeedContributor
    {
        Task SeedAsync(DataSeedContext context);
    }
}
```

- `IDataSeedContributor` 定义了 `SeedAsync` 方法用于执行 **数据种子逻辑**.

- 通常**检查数据库**是否已经存在种子数据.

- 你可以**注入**服务,检查数据播种所需的任何逻辑.

  

#### `IDataSeeder`服务:执行种子数据

**数据种子贡献者由ABP框架自动发现,并作为数据播种过程的一部分执行.**

如何自动执行种子数据呢？答案是：`IDataSeeder`服务

> 你可以通过依赖注入 `IDataSeeder` 并且在你需要时使用它初始化种子数据. 它内部调用 `IDataSeedContributor` 的实现去完成数据播种



修改项目` *.Domain`中的`BookStoreDbMigrationService`，依赖注入

```C#
 private readonly IDataSeeder _dataSeeder;
```

并如下使用执行种子数据

```C#
 await _dataSeeder.SeedAsync(tenant?.Id);
```

下面是修改后的完整代码如下：

```C#
public class BookStoreDbMigrationService : ITransientDependency
    {
        public ILogger<BookStoreDbMigrationService> Logger { get; set; }

        private readonly IDataSeeder _dataSeeder;
        private readonly IEnumerable<IBookStoreDbSchemaMigrator> _dbSchemaMigrators;

        public BookStoreDbMigrationService(
            IDataSeeder dataSeeder,
            IEnumerable<IBookStoreDbSchemaMigrator> dbSchemaMigrators
            )
        {
            _dataSeeder = dataSeeder;
            _dbSchemaMigrators = dbSchemaMigrators;

            Logger = NullLogger<BookStoreDbMigrationService>.Instance;
        }

        public async Task MigrateAsync()
        {
            Logger.LogInformation("Started database migrations...");

            await MigrateDatabaseSchemaAsync(); //执行数据库迁移
            await SeedDataAsync();  //执行种子数据

            Logger.LogInformation("Successfully completed database migrations.");
        }

        /// <summary>
        /// 执行数据库迁移
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        private async Task MigrateDatabaseSchemaAsync(Tenant tenant = null)
        {
            Logger.LogInformation(
                $"Migrating schema for {(tenant == null ? "host" : tenant.Name + " tenant")} database...");

            foreach (var migrator in _dbSchemaMigrators)
            {
                await migrator.MigrateAsync();
            }
        }

        /// <summary>
        /// 执行种子数据
        /// </summary>
        /// <param name = "tenant" ></ param >
        /// < returns ></ returns >
        private async Task SeedDataAsync(Tenant tenant = null)
        {
            Logger.LogInformation($"Executing {(tenant == null ? "host" : tenant.Name + " tenant")} database seed...");
            await _dataSeeder.SeedAsync(tenant?.Id);

        }
    }
```

设置控制台程序`*.DbMigrator`为启动项目，并运行，执行数据库迁移。

这时查看`Book`表,多了两条种子数据：

<img src="images/Abp%E5%AE%9E%E6%88%98%E4%B9%8BBookStore/image-20201208210723459.png" alt="image-20201208210723459" style="zoom: 80%;" />



#### dataSeeder.SeedAsync(tenant?.Id)干了啥？

`_dataSeeder`是个什么呢？

![image-20201208192119822](images/Abp%E5%AE%9E%E6%88%98%E4%B9%8BBookStore/image-20201208192119822.png)

相关源码如下：

[`DataSeederExtensions`](https://github.com/abpframework/abp/blob/42f37c5ff01ad853a5425d15539d4222cd0dab69/framework/src/Volo.Abp.Data/Volo/Abp/Data/DataSeederExtensions.cs)

```C#
using System;
using System.Threading.Tasks;

namespace Volo.Abp.Data
{
    public static class DataSeederExtensions
    {
        public static Task SeedAsync(this IDataSeeder seeder, Guid? tenantId = null)
        {
            return seeder.SeedAsync(new DataSeedContext(tenantId));
        }
    }
}
```

[DataSeedContext](https://github.com/abpframework/abp/blob/42f37c5ff01ad853a5425d15539d4222cd0dab69/framework/src/Volo.Abp.Data/Volo/Abp/Data/DataSeedContext.cs)

```C#
using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Volo.Abp.Data
{
    public class DataSeedContext
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Gets/sets a key-value on the <see cref="Properties"/>.
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <returns>
        /// Returns the value in the <see cref="Properties"/> dictionary by given <see cref="name"/>.
        /// Returns null if given <see cref="name"/> is not present in the <see cref="Properties"/> dictionary.
        /// </returns>
        [CanBeNull]
        public object this[string name]
        {
            get => Properties.GetOrDefault(name);
            set => Properties[name] = value;
        }

        /// <summary>
        /// Can be used to get/set custom properties.
        /// </summary>
        [NotNull]
        public Dictionary<string, object> Properties { get; }

        public DataSeedContext(Guid? tenantId = null)
        {
            TenantId = tenantId;
            Properties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Sets a property in the <see cref="Properties"/> dictionary.
        /// This is a shortcut for nested calls on this object.
        /// </summary>
        public virtual DataSeedContext WithProperty(string key, object value)
        {
            Properties[key] = value;
            return this;
        }
    }
}
```



[DataSeeder](https://github.com/abpframework/abp/blob/2c45d9d54cb4963b9a433de9e9d285dfc339bdde/framework/src/Volo.Abp.Data/Volo/Abp/Data/DataSeeder.cs)

```C#
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace Volo.Abp.Data
{
    //TODO: Create a Volo.Abp.Data.Seeding namespace?
    public class DataSeeder : IDataSeeder, ITransientDependency
    {
        protected IServiceScopeFactory ServiceScopeFactory { get; }
        protected AbpDataSeedOptions Options { get; }

        public DataSeeder(
            IOptions<AbpDataSeedOptions> options,
            IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
            Options = options.Value;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            using (var scope = ServiceScopeFactory.CreateScope())
            {
                foreach (var contributorType in Options.Contributors)
                {
                    var contributor = (IDataSeedContributor) scope
                        .ServiceProvider
                        .GetRequiredService(contributorType);

                    await contributor.SeedAsync(context);
                }
            }
        }
    }
}
```

综上可知：

**`IDataSeeder`它内部调用 `IDataSeedContributor` 的`SeedAsync`方法去完成数据播种**



## 1.6 *.Application.Contracts 项目

### 应用服务层

应用服务实现应用程序的**用例**, 将**领域层逻辑公开给表示层**.

从表示层(可选)调用应用服务,**DTO ([数据传对象](https://docs.abp.io/zh-Hans/abp/latest/Data-Transfer-Objects))** 作为参数. 返回(可选)DTO给表示层.



创建一个.NetCore类库项目

### 基本设置

- 修改默认命名空间为`Zto.BookStore`

- 创建文件夹`Books`

### 项目引用

- `*.Domain.Shared`



### 依赖包

- `*.Volo.Abp.Ddd.Application.Contracts`



### 创建`AbpModule`

在文件夹`Books`下创建`AbpModule`:

```C#
using Volo.Abp.Modularity;

namespace Zto.BookStore
{
    [DependsOn(
     typeof(BookStoreDomainSharedModule)
        )]
    public class BookStoreApplicationContractsModule : AbpModule
    {

    }
}
```



### DTO

在文件夹`Books`下创建Dto:

#### `BooksDto`

```C#
using System;
using Volo.Abp.Application.Dtos;

namespace Zto.BookStore.Books
{
    public class BookDto : AuditedEntityDto<Guid>
    {
        public Guid AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}
```

- **DTO**类被用来在 **表示层** 和 **应用层** **传递数据**.查看[DTO文档](https://docs.abp.io/zh-Hans/abp/latest/Data-Transfer-Objects)查看更多信息.
- 为了在页面上展示书籍信息,`BookDto`被用来将书籍数据传递到表示层.
- `BookDto`继承自 `AuditedEntityDto<Guid>`.跟上面定义的 `Book` 实体一样具有一些审计属性.



#### `CreateUpdateBookDto`

```C#
using System;
using System.ComponentModel.DataAnnotations;


namespace Zto.BookStore.Books
{
    public class CreateUpdateBookDto
    {
        public Guid AuthorId { get; set; }

        [Required]
        [StringLength(BookConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public BookType Type { get; set; } = BookType.Undefined;

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Required]
        public float Price { get; set; }
    }
}

```

- 这个DTO类被用于在创建或更新书籍的时候从用户界面获取图书信息.
- 它定义了数据注释属性(如`[Required]`)来定义属性的验证. DTO由ABP框架[自动验证](https://docs.abp.io/zh-Hans/abp/latest/Validation).



### IBookAppService

```C#
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Zto.BookStore.Books
{
    public interface IBookAppService:
           ICrudAppService<     //Defines CRUD methods
            BookDto,            //Used to show books
            Guid,               //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto>            //Used to create/update a book
    {

    }
}
```

继承`ICrudAppService<>`



## 1.7 *.BookStore.Application 项目

创建一个.NetCore类库项目

### 基本设置

- 修改默认命名空间为`Zto.BookStore`

- 创建文件夹`Books`

### 项目引用

- `*.Application.Contracts`



### 依赖包

- `Volo.Abp.Ddd.Application`



### 创建`AbpModule`

在文件夹`Books`下创建`AbpModule`:

```C#
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Zto.BookStore
{
    [DependsOn(
        typeof(BookStoreDomainModule),
        typeof(BookStoreApplicationContractsModule),
         typeof(AbpLocalizationModule)
        )]
    public class BookStoreApplicationModule : AbpModule
    {
    }
}

```

特别指出的是，依赖模块`AbpLocalizationModule`,支持本地化



### 对象映射

#### 知识点 AutoMap

[文档](https://docs.abp.io/zh-Hans/abp/latest/Object-To-Object-Mapping)

[AutoMapper——Map之实体的桥梁](https://blog.csdn.net/u011978814/article/details/62225980)

[AutoMapper官网](http://automapper.org/)

[官方文档](https://docs.automapper.org/en/latest/Getting-started.html)

##### 基本使用

- **[Setup](https://docs.automapper.org/en/latest/Setup.html)**

```C#
var config = new MapperConfiguration(cfg => {
    cfg.AddProfile<AppProfile>();
    cfg.CreateMap<Source, Dest>();
});

var mapper = config.CreateMapper();
// or
IMapper mapper = new Mapper(config);
var dest = mapper.Map<Source, Dest>(new Source());
```

Starting with 9.0, the static API is no longer available.

- **Gathering configuration before initialization**

AutoMapper also lets you gather configuration before initialization:

```
var cfg = new MapperConfigurationExpression();
cfg.CreateMap<Source, Dest>();
cfg.AddProfile<MyProfile>();
MyBootstrapper.InitAutoMapper(cfg);

var mapperConfig = new MapperConfiguration(cfg);
IMapper mapper = new Mapper(mapperConfig);
```

- **Profile Instances**

A good way to organize your mapping configurations is with profiles. Create classes that inherit from `Profile` and put the configuration in the constructor:

（通过自定义``Profile` `的子类，设置映射配置）

```c#
// This is the approach starting with version 5
public class OrganizationProfile : Profile
{
	public OrganizationProfile()
	{
		CreateMap<Foo, FooDto>();
		// Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
	}
}
```

- **Assembly Scanning for auto configuration**

Profiles can be added to the main mapper configuration in a number of ways, either directly:

（通过`AddProfile`将自定义``Profile` `的子类添加到映射配置中）

```C#
cfg.AddProfile<OrganizationProfile>();
cfg.AddProfile(new OrganizationProfile());
```

or by automatically scanning for profiles:

（通过程序集扫描profiles类到映射配置中）

```C#
// Scan for all profiles in an assembly
// ... using instance approach:

var config = new MapperConfiguration(cfg => {
    cfg.AddMaps(myAssembly);
});
var configuration = new MapperConfiguration(cfg => cfg.AddMaps(myAssembly));

// Can also use assembly names:
var configuration = new MapperConfiguration(cfg =>
    cfg.AddMaps(new [] {
        "Foo.UI",
        "Foo.Core"
    });
);

// Or marker types for assemblies:
var configuration = new MapperConfiguration(cfg =>
    cfg.AddMaps(new [] {
        typeof(HomeController),
        typeof(Entity)
    });
);
```

AutoMapper will scan the designated assemblies for classes inheriting from Profile and add them to the configuration.



#### 配置对象映射关系

在将`Book`返回到表示层时,需要将`Book`实体转换为`BookDto`对象. [AutoMapper](https://automapper.org/)库可以在定义了正确的映射时自动执行此转换.

因此你只需在`*.BookStore.Application`项目的中:

中定义映射:

- 第一步：自定义`BookStoreApplicationAutoMapperProfile`继承自` Profile`，对象映射配置都在这里设置

`BookStoreApplicationAutoMapperProfile.cs`

```C#
    public class BookStoreApplicationAutoMapperProfile : Profile
    {
        public BookStoreApplicationAutoMapperProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<CreateUpdateBookDto, Book>();
        }
    }
```

- 第二步：配置`AbpAutoMapperOptions`

  使`BookStoreApplicationModule`模块依赖`AbpAutoMapperModule`模块，并在的`ConfigureServices`方法中配置`AbpAutoMapperOptions`，本示例是通过扫描程序集的方式搜索`Porfile`类，并添加到AutoMapper配置中

  ```C#
  using Volo.Abp.AutoMapper;
  using Volo.Abp.Localization;
  using Volo.Abp.Modularity;
  
  namespace Zto.BookStore
  {
      [DependsOn(
          ...
          typeof(AbpAutoMapperModule)
          )]
      public class BookStoreApplicationModule : AbpModule
      {
          public override void ConfigureServices(ServiceConfigurationContext context)
          {
              Configure<AbpAutoMapperOptions>(options =>
              {
                  //通过扫描程序集的方式搜索`Porfile`类，并添加到AutoMapper配置中
                  options.AddMaps<BookStoreApplicationModule>(); 
              });
          }
      }
  }
  ```

##### 源码代码分析

以下代码：

```C#
options.AddMaps<BookStoreApplicationModule>(); 
```

  调用源码：

```C#
   public class AbpAutoMapperOptions
   {
        public AbpAutoMapperOptions()
        {
            Configurators = new List<Action<IAbpAutoMapperConfigurationContext>>();
            ValidatingProfiles = new TypeList<Profile>();
        }
       
       public void AddMaps<TModule>(bool validate = false)
        {
            var assembly = typeof(TModule).Assembly;

            Configurators.Add(context =>
            {
                context.MapperConfiguration.AddMaps(assembly);
            });
           
            ......
   }
```

这里使用

```C#
context.MapperConfiguration.AddMaps(assembly);
```

扫描程序集的方式搜索`Profile`类添加到`AutoMapper`配置中



### 对象转换

配置对象映射关系后，可以使用如下代码进行对象转换：

```C#
 var bookDto = ObjectMapper.Map<Book, BookDto>(book);
 var bookDtos = ObjectMapper.Map<List<Book>, List<BookDto>>(books)
```

其中，

`ObjectMappers` 是`ApplicationService`类内置的对象，只要`xxxAppService`继承自`ApplicationService`即可使用



#### 源码分析

`IObjectMapper`:

```C#
namespace Volo.Abp.ObjectMapping
{
    //
    // 摘要:
    //     Defines a simple interface to automatically map objects.
    public interface IObjectMapper
    {
        //
        // 摘要:
        //     Gets the underlying Volo.Abp.ObjectMapping.IAutoObjectMappingProvider object
        //     that is used for auto object mapping.
        IAutoObjectMappingProvider AutoObjectMappingProvider
        {
            get;
        }
        TDestination Map<TSource, TDestination>(TSource source); //A
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);//A
    }
}
```

在模块`AbpObjectMappingModule`

```C#
public class AbpObjectMappingModule : AbpModule
 {
        ......
            
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(
                typeof(IObjectMapper<>),
                typeof(DefaultObjectMapper<>)
            );
        }
  }
```

设置了`IObjectMapper`的默认实现类`DefaultObjectMapper`

```C#
   public class DefaultObjectMapper : IObjectMapper, ITransientDependency
   {
        public IAutoObjectMappingProvider AutoObjectMappingProvider { get; }
       
        public virtual TDestination Map<TSource, TDestination>(TSource source)
        {
            .....

            return AutoMap(source, destination);
        }
       public virtual TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            ....
            return AutoMap(source, destination);
        }
       
        protected virtual TDestination AutoMap<TSource, TDestination>(object source)
        {
            return AutoObjectMappingProvider.Map<TSource, TDestination>(source);
        }

        protected virtual TDestination AutoMap<TSource, TDestination>(TSource source, TDestination destination)
        {
            return AutoObjectMappingProvider.Map<TSource, TDestination>(source, destination);
        }
   }
```

​           根据以上代码可以看出：`ObjectMapper.Map<S,D>()`最终调用的都是

```C#
AutoObjectMappingProvider.Map<TSource, TDestination>(source);
or
AutoObjectMappingProvider.Map<TSource, TDestination>(source, destination);
```

-->`IAutoObjectMappingProvider AutoObjectMappingProvider`-->`AutoMapperAutoObjectMappingProvider`

```C#
  public class AutoMapperAutoObjectMappingProvider : IAutoObjectMappingProvider
  {
        public IMapperAccessor MapperAccessor { get; }
      
        public virtual TDestination Map<TSource, TDestination>(object source)
        {
            return MapperAccessor.Mapper.Map<TDestination>(source); //B
        }

        public virtual TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return MapperAccessor.Mapper.Map(source, destination);  //B
        }
  }
```

-->`IMapperAccessor MapperAccessor`

```C#
    public interface IMapperAccessor
    {
        IMapper Mapper { get; }
    }
```

-->即调用的是`MapperAccessor.Mapper`的`Map()`方法,

那`MapperAccessor.Mapper`到底是谁呢?

-->`AbpAutoMapperModule`模块

```C#
    [DependsOn(
        typeof(AbpObjectMappingModule),
        typeof(AbpObjectExtendingModule),
        ....
        )]
    public class AbpAutoMapperModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper();

            var mapperAccessor = new MapperAccessor();
            context.Services.AddSingleton<IMapperAccessor>(_ => mapperAccessor);
            context.Services.AddSingleton<MapperAccessor>(_ => mapperAccessor);
        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            CreateMappings(context.ServiceProvider);
        }
        
         private void CreateMappings(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var options = scope.ServiceProvider.GetRequiredService<IOptions<AbpAutoMapperOptions>>().Value;
                ......
                var mapperConfiguration = new MapperConfiguration(mapperConfigurationExpression =>
                {
                    ConfigureAll(new AbpAutoMapperConfigurationContext(mapperConfigurationExpression, scope.ServiceProvider));
                });
               ......
                 var mapperConfiguration = new MapperConfiguration(
                {
                    ....
                });
                scope.ServiceProvider.GetRequiredService<MapperAccessor>().Mapper = mapperConfiguration.CreateMapper(); //C
            }
        }

```

-->`  var mapperAccessor = new MapperAccessor();`注册了单例

-->`scope.ServiceProvider.GetRequiredService<MapperAccessor>().Mapper = mapperConfiguration.CreateMapper();`

这样步骤**C**的代码使得步骤**B**中的`MapperAccessor.Mapper`（其类型为：`Volo.Abp.AutoMapper.IMapperAccessor`）得到了实例化



**综上所有步骤，等价于**

```C#
AutoMapperAutoObjectMappingProvider.MapperAccessor.Mapper = mapperConfiguration.CreateMapper(); 
```

  这就是我们熟悉的：

```C#
var config = new MapperConfiguration(cfg => {
    cfg.AddProfile<AppProfile>();
    cfg.CreateMap<Source, Dest>();
});

IMapper mapper = config.CreateMapper();
var dest = mapper.Map<Source, Dest>(new Source());
```



### BookStoreAppService

在文件夹`Books`下创建`BookStoreAppService.cs`

这是一个抽象类，其它`xxxApplicationService`都将继续自它：

```C#
    /// <summary>
    /// Inherit your application services from this class.
    /// </summary>
    public abstract class BookStoreAppService : ApplicationService
    {
        protected BookStoreAppService()
        {
            LocalizationResource = typeof(BookStoreResource);
        }
    }
```

#### 设置本地化资源

```c#
LocalizationResource = typeof(BookStoreResource);
```



### BookAppService.cs

`BookAppService`继承上一节定义的抽象类`BookStoreAppService`，

```C#
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Zto.BookStore.Books
{
    public class BookAppService :
            CrudAppService<
                Book,                //The Book entity
                BookDto,             //Used to show books
                Guid,                //Primary key of the book entity
                PagedAndSortedResultRequestDto, //Used for paging/sorting
                CreateUpdateBookDto>,           //Used to create/update a book
            IBookAppService                     //implement the IBookAppService
    {

        public BookAppService(IRepository<Book, Guid> repository)
            : base(repository)
        {
        }

    }
}
```





## 1.8  *.HttpApi 项目

**用于定义API控制器.**

大多数情况下,你不需要手动定义API控制器,因为ABP的[动态API](https://docs.abp.io/zh-Hans/abp/latest/API/Auto-API-Controllers)功能会根据你的应用层自动创建API控制器. 但是,如果你需要编写API控制器,那么它是最合适的地方.

- 它依赖 `.Application.Contracts` 项目,因为它需要注入应用服务接口.



创建一个.NetCore类库项目

### 基本设置

- 修改默认命名空间为`Zto.BookStore`

  


### 项目引用

- `*.Application.Contracts`: 注意哦，不是：`*.Application`



### 依赖包

- `Volo.Abp.AspNetCore.Mvc`



### 创建`AbpModule

```C#
using Volo.Abp.Modularity;

namespace Zto.BookStore
{
    [DependsOn(
        typeof(BookStoreApplicationContractsModule)
        )]
    public class BookStoreHttpApiModule : AbpModule
    {
    }
}
```



### Controllers

​		创建`Controllers`文件夹，并在其中创建一个`BookStoreController`,继承直`AbpController`

```C#
using Volo.Abp.AspNetCore.Mvc;
using Zto.BookStore.Localization;

namespace Zto.BookStore.Controllers
{
    /* Inherit your controllers from this class.
    */
    public abstract class BookStoreController : AbpController
    {
        protected BookStoreController()
        {
            LocalizationResource = typeof(BookStoreResource);
        }
    }
}

```



## 1.9 *.HttpApi.Client 项目

定义C#客户端代理使用解决方案的HTTP API项目. 可以将上编辑共享给第三方客户端,使其轻松的在DotNet应用程序中使用你的HTTP API(其他类型的应用程序可以手动或使用其平台的工具来使用你的API).

ABP有[动态 C# API 客户端](https://docs.abp.io/zh-Hans/abp/latest/API/Dynamic-CSharp-API-Clients)功能,所以大多数情况下你不需要手动的创建C#客户端代理.

`.HttpApi.Client.ConsoleTestApp` 项目是一个用于演示客户端代理用法的控制台应用程序.

- 它依赖 `.Application.Contracts` 项目,因为它需要使用应用服务接口和DTO.

> 如果你不需要为API创建动态C#客户端代理,可以删除此项目和依赖项



**综上所述，`BookStore`项目目前并没有打算给第三方客户端提供Api，先创建该项目，然后将可其卸载**



这个项目的意义就是了为了满足类型如下的场景应运而生的

一个**第三方客户端App**

或者在微服务架构中其它开发团队开发的其它模块。

他们的共同需求就是

- 也是使用.Net技术

- 想使用**BooksStore**在项目`Application.Contracts`定义的接口服务



我们`BookStore`项目组，只是提供`*.HttpApi.Client`项目生成的`.dll`即可，其它项目直接已入这个`.dll`,就可以像调用本地的实例对象一样调用远程Api。

这种场景，就相当于阿里云的云服务提供的基于`**.Net Standard 2.0**的**SDK**



创建一个**.Net Standard 2.0**的类库项目

### 基本设置

- 目标框架为：**.Net Standard 2.0**

  https://docs.microsoft.com/zh-cn/dotnet/standard/net-standard#net-5-and-net-standard

  > 如果你不需要支持 .NET Framework，可以选择 .NET Standard 2.1 或 .NET 5。 我们建议你跳过 .NET Standard 2.1，而直接选择 .NET 5。 大多数广泛使用的库最终都将同时以 .NET Standard 2.0 和 .NET 5 作为目标。 支持 .NET Standard 2.0 可提供最大的覆盖范围，而支持 .NET 5 可确保你可以为已使用 .NET 5 的客户利用最新的平台功能。

​       

​       本示例是基于目前最新的`.Net5.0`, 该项目的目标框架设置为`.Net Standard 2.0`报错：

```markdown
项目“..\Zto.BookStore.Application.Contracts\Zto.BookStore.Application.Contracts.csproj”指向“net5.0”。它不能被指向“.NETStandard,Version=v2.0”的项目引用。	Zto.BookStore.HttpApi.Client	C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\Microsoft.Common.CurrentVersion.targets	1662	

```

**目前没有什么好的解决办法，故将该项目的目标框架设置改为`.Net5`**， 右键项目文件，选择【编辑项目文件】

```xml
  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
    //......
  </PropertyGroup>
```

修改为：

```xml
  <PropertyGroup>
    <TargetFramework>.net5.0</TargetFramework>
    //......
  </PropertyGroup>
```



- 修改默认命名空间为`Zto.BookStore`

  

### 项目引用

- `*.Application.Contracts`: 注意哦，不是：`*.Application`



### 依赖包

- `Volo.Abp.Http.Client`：有动态 C# API 客户端的功能



### 创建`AbpModule`

```C#
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Zto.BookStore
{
    [DependsOn(
        typeof(BookStoreApplicationContractsModule), //包含应用服务接口
        typeof(AbpHttpClientModule)                  //用来创建客户端代理
    )]
    public class BookStoreHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "BookStore";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //创建动态客户端代理
            context.Services.AddHttpClientProxies(
                typeof(BookStoreApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}

```

**注意事项**：

这里的

```C#
public const string RemoteServiceName = "BookStore";
```

定义了服务的名称，这就要求直接引用`*.HttpApi.Client` 项目或其生成的`*.HttpApi.Client.dll`的第三方项目(如：下面要创建的*.HttpApi.Client.ConsoleTestApp 测试项目)在配置文件`appsettings.json`的`RemoteServices`节点也要定义一个**名为`BookStore`服务配置节点**，如下所示：

`*.HttpApi.Client.ConsoleTestApp` 测试项目的`appsettings.json`:

```json
{
  "RemoteServices": {
    "BookStore": {
      "BaseUrl": "https://localhost:8000"
    }
  }
}
```

特别注意：

- `*.HttpApi.Client.ConsoleTestApp` 测试项目配置文件中的``appsettings.json`的

  ```json
      "BookStore": {
        ....
      }
  ```

  要求必须与`*.HttpApi`项目中的模块`BookStoreHttpApiClientModule`定义的`RemoteServiceName`

  ```C#
      public class BookStoreHttpApiClientModule : AbpModule
      {
          public const string RemoteServiceName = "BookStore";
          //......
      }
  ```

  相同。

- 配置文件中的

  ```json
  "BaseUrl": "https://localhost:8000"
  ```

  是接下来我们要创建的`.BookStore.HttpApi.Host`项目的网站地址

### 测试程序

​       看完这一节，直接跳转到章节【1.11 *.HttpApi.Client.ConsoleTestApp 测试项目】进行测试



## 1.10 *.BookStore.HttpApi.Host 项目

这是一个用于发布部署WebApi的Web应用程序。

在解决方案的`src`目录下，新建一个 基于Asp.Net Core 的WebApi应用程序。

### 项目引用

- `*.HttpApi`: 因为UI层需要使用解决方案的API和应用服务接口.
- `*.Application`
- `*.EntityFrameworkCore.DbMigrations`: 



### 依赖包

- `Volo.Abp.Autofac`
- `Volo.Abp.AspNetCore.Serilog`
- `Volo.Abp.Caching.StackExchangeRedis`
- `Volo.Abp.Swashbuckle`
- `Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared` :错误页面UI

- `Microsoft.AspNetCore.DataProtection.StackExchangeRedis`

### 修改端口

在`launchSettings.json`文件中修改应用程序启动端口

- https:44327
- http:44328

```C#
{
     //......
    "launchUrl": "weatherforecast",
    //......
    "iisExpress": {
      "launchUrl": "weatherforecast",
      "applicationUrl": "http://localhost:12016",
      "sslPort": 44315
    }
  },

    "Zto.BookStore.HttpApi.Host": {
      "launchUrl": "weatherforecast", 
      "applicationUrl": "https://localhost:5001;http://localhost:5000",
       //......
 }
```

修改为：

```C#
{
    
   //......
        "launchUrl": "Home",
    //......
    "iisExpress": {
      "launchUrl": "Home",
      "applicationUrl": "http://localhost:8001",
      "sslPort": 8000
    }
  },
  //......
      "applicationUrl": "https://localhost:8000;http://localhost:8001",
  //......
}

```

### 配置文件

`appsetting.json`:

```json
{
  "App": {
    "CorsOrigins": "https://*.BookStore.com,http://localhost:4200,https://localhost:44307"
  },
  "ConnectionStrings": {
    "Default": "Server=.;Database=BookStore_Zto;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Redis": {
    "Configuration": "127.0.0.1"
  },
  "AuthServer": {
    "Authority": "https://localhost:44388",
    "RequireHttpsMetadata": "true",
    "SwaggerClientId": "BookStore_Swagger",
    "SwaggerClientSecret": "1q2w3e*"
  },
  "StringEncryption": {
    "DefaultPassPhrase": "iIpMRCMOnSTU6lxK"
  },
  "Settings": {
    "Abp.Mailing.Smtp.Host": "127.0.0.1",
    "Abp.Mailing.Smtp.Port": "25",
    "Abp.Mailing.Smtp.UserName": "",
    "Abp.Mailing.Smtp.Password": "",
    "Abp.Mailing.Smtp.Domain": "",
    "Abp.Mailing.Smtp.EnableSsl": "false",
    "Abp.Mailing.Smtp.UseDefaultCredentials": "true",
    "Abp.Mailing.DefaultFromAddress": "noreply@abp.io",
    "Abp.Mailing.DefaultFromDisplayName": "ABP application"
  }
}

```



编写相应的功能前，我们得改造下`Program.cs`和`Startup.cs`

### Program.cs

```C#
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Zto.BookStore
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
#if DEBUG
                .WriteTo.Async(c => c.Console())
#endif
                .CreateLogger();

            try
            {
                Log.Information("Starting Zto.BookStore.HttpApi.Host.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseAutofac()
                .UseSerilog();
    }
}

```

       - .UseAutofac()：使用Autofac
       - .UseSerilog(): 使用UseSerilog日志



### Startup.cs

```C#
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Zto.BookStore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<BookStoreHttpApiHostModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
   }
}
```

- 添加`BookStoreHttpApiHostModule`模块
- 使用`InitializeApplication`初始化应用程序

### 创建`AbpModule`

`BookStoreHttpApiHostModule.cs`

#### 配置Services

```C#
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Modularity;
using Zto.BookStore.EntityFrameworkCore;

namespace Zto.BookStore
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAuthorizationModule),
        typeof(BookStoreApplicationModule),
        typeof(BookStoreHttpApiModule),
        typeof(AbpAspNetCoreMvcUiModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(BookStoreEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule)
     )]
    public class BookStoreHttpApiHostModule : AbpModule
    {
       private const string DefaultCorsPolicyName = "Default";

        //配置Services
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            
            ConfigureConventionalControllers();
            ConfigureAuthentication(context, configuration);
            ConfigureLocalization();
            ConfigureCache(configuration);
            ConfigureVirtualFileSystem(context);
            ConfigureRedis(context, configuration, hostingEnvironment);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context);
          
        }
        
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            //在这里配置中间件
        }
    }
}
```

这是`BookStoreHttpApiHostModule`的基本框架，下面将一步步添加相应的功能



##### ConfigureConventionalControllers

```C#
        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                //自动生成API控制器
                options.ConventionalControllers.Create(typeof(BookStoreApplicationModule).Assembly);
            });
        }
```

上述代码让ABP可以按照惯例 **自动** 生成API控制器。



###### 自动API控制器

[官方文档](https://docs.abp.io/zh-Hans/abp/latest/API/Auto-API-Controllers)

> 建[应用程序服务](https://docs.abp.io/zh-Hans/abp/latest/API/Application-Services)后, 通常需要创建API控制器以将此服务公开为HTTP(REST)API端点. 典型的API控制器除了将方法调用重定向到应用程序服务并使用[HttpGet],[HttpPost],[Route]等属性配置REST API之外什么都不做.
>
> ABP可以按照惯例 **自动** 将你的应用程序服务配置为API控制器. 大多数时候你不关心它的详细配置,但它可以完全被自定义.



##### ConfigureAuthentication

配置认证

```C#
        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.Audience = "BookStore";
                });
        }
```



##### ConfigureLocalization

本地化

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            });
        }



##### ConfigureCache

缓存配置

```C#
        private void ConfigureCache(IConfiguration configuration)
        {
            Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "BookStore:"; });
        }
```



##### ConfigureVirtualFileSystem

虚拟文件系统

```C#
   private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<BookStoreDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Zto.BookStore.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<BookStoreDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Zto.BookStore.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<BookStoreApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Zto.BookStore.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<BookStoreApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Zto.BookStore.Application"));
            });
        }
    }
```


##### ConfigureRedis

Redis

```C#
        private void ConfigureRedis(ServiceConfigurationContext context,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                context.Services
                    .AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "BookStore-Protection-Keys");
            }
        }
```



##### ConfigureCors

跨越

```C#
        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
```



##### ConfigureSwaggerServices

配置Swagger

```C#
private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAbpSwaggerGenWithOAuth(
                configuration["AuthServer:Authority"],
                new Dictionary<string, string>
                {
                    {"BookStore", "BookStore API"}
                },
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                });
        }
```



#### 配置中间件

在

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
    
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
    
            app.UseAbpRequestLocalization();
    
            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }
    
            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();
    
            if (MultiTenancyConsts.IsEnabled)
            {
                //app.UseMultiTenancy();//暂时不支持多租户
            }
    
            app.UseAuthorization();
    
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API");
    
                var configuration = context.GetConfiguration();
                options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            });
    
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }


### HomeController

在`Controllers`文件夹下，创建`HomeController.cs`

```c#
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Zto.BookStore.Controllers
{
    public class HomeController : AbpController
    {
        public ActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}

```



### 运行HttApi.Host

运行WebApiHost网站：跳转到**swagger**的首页：

> Tips：
>
> 如果出现：Failed to load API definition.
>
> 可以访问：打开http://localhost:<port>/swagger/v1/swagger.json，查看错误信息，排除问题



<img src="images/Abp%E5%AE%9E%E6%88%98%E4%B9%8BBookStore/image-20201210202841427.png" alt="image-20201210202841427" style="zoom:100%;" />



访问

```markdown
https://localhost:8000/api/app/book
```

返回(我们之前插入的种子数据)：

```json
{
  "totalCount": 2,
  "items": [
    {
      "authorId": "00000000-0000-0000-0000-000000000000",
      "authorName": null,
      "name": "The Hitchhiker's Guide to the Galaxy",
      "type": 7,
      "publishDate": "1995-09-27T00:00:00",
      "price": 42,
      "lastModificationTime": null,
      "lastModifierId": null,
      "creationTime": "2020-12-08T22:17:08.6454076",
      "creatorId": null,
      "id": "ac1c9ff8-551e-4f97-9594-d50ed4f4f594"
    },
    {
      "authorId": "00000000-0000-0000-0000-000000000000",
      "authorName": null,
      "name": "1984",
      "type": 3,
      "publishDate": "1949-06-08T00:00:00",
      "price": 19.84,
      "lastModificationTime": null,
      "lastModifierId": null,
      "creationTime": "2020-12-08T22:17:08.4731128",
      "creatorId": null,
      "id": "f27890cb-f01b-4965-b2af-19f3bacc1e40"
    }
  ]
}
```



但是如果我们插入一个`Book`对象

Curl

```powershell
curl -X POST "https://localhost:8000/api/app/book" -H "accept: text/plain" -H "Content-Type: application/json" -d "{\"authorId\":\"3fa85f64-5717-4562-b3fc-2c963f66afa6\",\"name\":\"string\",\"type\":0,\"publishDate\":\"2020-12-10\",\"price\":0}"
```

Request URL

```
https://localhost:8000/api/app/book
```

Server response

| Code              | Details                                                      |
| ----------------- | ------------------------------------------------------------ |
| 400*Undocumented* | Error:Response headers` content-length: 0  date: Thu10 Dec 2020 12:37:51 GMT  server: Kestrel  status: 400  x-correlation-id: ff1b2a0878fa42fca971bffcfd0e570f ` |

返回错误码：400，表示没有授权。授权我们将在`*.IdentityServer`项目中相应的功能



## 1.11 *.HttpApi.Client.ConsoleTestApp 测试项目

这是一个用于演示客户端代理用法的控制台应用程序。

在解决方案的`test`目录下，新建一个.Net的控制台项目

### 项目引用

- `*.Application.Contracts`: 注意，并没有引用项目`.Application`，只依赖接口

 

### 依赖包

- `Microsoft.Extensions.Hosting`



### 发布*.BookStore.HttpApi.Host 项目

为了测试，我们先做如下准备:

第一步：，我们先把`*.BookStore.HttpApi.Host `项目发布到**IIS**，地址及其端口如下：

- https://localhost:8100 

没有证书，可以选择**IIS ExPress Development Certificate**证书：

<img src="images/Abp%E5%AE%9E%E6%88%98%E4%B9%8BBookStore/image-20201211094148878.png" alt="image-20201211094148878" style="zoom:66%;" />



- http://localhost:8101



第二步：修改远程服务地址

添加`*.HttpApi.Client.ConsoleTestApp` 测试项目的配置`appsettings.json`:

```C#
{
  "RemoteServices": {
    "BookStore": {
      "BaseUrl": "https://localhost:8100"
    }
  }
}
```

- `BookStore`就是在创建客户端代码模块`BookStoreHttpApiClientModule`时，给定的`RemoteServiceName`的值，

  **两者必须一致**

  见代码：

-     [DependsOn(
          typeof(BookStoreApplicationContractsModule), //包含应用服务接口
          typeof(AbpHttpClientModule)                  //用来创建客户端代理
      )]
      public class BookStoreHttpApiClientModule : AbpModule
      {
          public const string RemoteServiceName = "BookStore";
      
          public override void ConfigureServices(ServiceConfigurationContext context)
          {
              //创建动态客户端代理
              context.Services.AddHttpClientProxies(
                  typeof(BookStoreApplicationContractsModule).Assembly,
                  RemoteServiceName
              );
          }
      }

- `"BaseUrl": "http://localhost:8101"`就是第一步中`*.BookStore.HttpApi.Host `项目的IIS发布地址



### 创建AbpModule

`BookStoreConsoleApiClientModule`

```C#
    [DependsOn(
        typeof(BookStoreHttpApiClientModule)
        )]
    public class BookStoreConsoleApiClientModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            //客户端代理配置，可无
            PreConfigure<AbpHttpClientBuilderOptions>(options =>
            {
                options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) =>
                {
                    clientBuilder.AddTransientHttpErrorPolicy(
                        policyBuilder => policyBuilder.WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i)))
                    );
                });
            });
        }
    }
```

依赖模块`BookStoreHttpApiClientModule`

### 创建宿主服务

- 创建宿主服务`ConsoleTestAppHostedService`, 用于承载客户端Demo类`ClientDemoService`:

```C#
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;

namespace Zto.BookStore.HttpApi.Client.ConsoleTestApp
{
    public class ConsoleTestAppHostedService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = AbpApplicationFactory.Create<BookStoreConsoleApiClientModule>())
            {
                application.Initialize();

                var demo = application.ServiceProvider.GetRequiredService<ClientDemoService>();
                await demo.RunAsync();

                application.Shutdown();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

```

- 添加宿主服务

  在`Program.cs`添加宿主服务：

  ```C#
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using System.Threading.Tasks;
  
  namespace Zto.BookStore.HttpApi.Client.ConsoleTestApp
  {
      class Program
      {
          static async Task Main(string[] args)
          {
              await CreateHostBuilder(args).RunConsoleAsync();
          }
  
          public static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args)
                  .ConfigureServices((hostContext, services) =>
                  {
                      services.AddHostedService<ConsoleTestAppHostedService>();
                  });
      }
  }
  
  ```

  

### 创建客户端Demo

`ClientDemoService`用于模拟客户端，通过调用客户端代理模块【`BookStoreHttpApiClientModule`】：

```C#
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Zto.BookStore.Books;

namespace Zto.BookStore.HttpApi.Client.ConsoleTestApp
{
    public class ClientDemoService : ITransientDependency
    {
        private readonly IBookAppService _bookAppService;

        public ClientDemoService(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public async Task RunAsync()
        {
            var requstDto = new PagedAndSortedResultRequestDto
            {
                Sorting = "PublishDate desc"
            };

            PagedResultDto<BookDto> output = await _bookAppService.GetListAsync(requstDto);
            Console.WriteLine($"BookList:{JsonConvert.SerializeObject(output)}");
        }
    }
}

```

可以看到，客户端Demo可以像调用本地类库一样调用远程服务。



### 测试远程调用

将`*.HttpApi.Client.ConsoleTestApp`测试项目设置为启动项，运行。

输入如下：

```powershell
BookList:{"TotalCount":2,"Items":[{"AuthorId":"00000000-0000-0000-0000-000000000000","AuthorName":null,"Name":"The Hitchhiker's Guide to the Galaxy","Type":7,"PublishDate":"1995-09-27T00:00:00","Price":42.0,"LastModificationTime":null,"LastModifierId":null,"CreationTime":"2020-12-10T21:25:56.4359053","CreatorId":null,"Id":"fc013530-19df-44b9-8272-0a664a8178fb"},{"AuthorId":"00000000-0000-0000-0000-000000000000","AuthorName":null,"Name":"1984","Type":3,"PublishDate":"1949-06-08T00:00:00","Price":19.84,"LastModificationTime":null,"LastModifierId":null,"CreationTime":"2020-12-10T21:25:56.2250498","CreatorId":null,"Id":"e4738098-fecc-4486-a1d3-659d1947a13e"}]}

//.......
```

即：

```C#
{
  "TotalCount": 2,
  "Items": [
    {
      "AuthorId": "00000000-0000-0000-0000-000000000000",
      "AuthorName": null,
      "Name": "The Hitchhiker's Guide to the Galaxy",
      "Type": 7,
      "PublishDate": "1995-09-27T00:00:00",
      "Price": 42.0,
      "LastModificationTime": null,
      "LastModifierId": null,
      "CreationTime": "2020-12-10T21:25:56.4359053",
      "CreatorId": null,
      "Id": "fc013530-19df-44b9-8272-0a664a8178fb"
    },
    {
      "AuthorId": "00000000-0000-0000-0000-000000000000",
      "AuthorName": null,
      "Name": "1984",
      "Type": 3,
      "PublishDate": "1949-06-08T00:00:00",
      "Price": 19.84,
      "LastModificationTime": null,
      "LastModifierId": null,
      "CreationTime": "2020-12-10T21:25:56.2250498",
      "CreatorId": null,
      "Id": "e4738098-fecc-4486-a1d3-659d1947a13e"
    }
  ]
}
```



## 1.12 `*.IdentityServer`

添加一个Asp.Net WebAPI应用程序

### 项目引用

 ```xml
    <PackageReference Include="Serilog.AspNetCore" Version="3.2.0" />
    <PackageReference Include="Serilog.Sinks.Async" Version="1.4.0" />
    <PackageReference Include="Microsoft.AspNetCore.DataProtection.StackExchangeRedis" Version="5.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="5.0.0" />

    <PackageReference Include="Volo.Abp.Autofac" Version="4.0.0" />
    <PackageReference Include="Volo.Abp.Caching.StackExchangeRedis" Version="4.0.0" />
    <PackageReference Include="Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic" Version="4.0.0" />
    <PackageReference Include="Volo.Abp.AspNetCore.Serilog" Version="4.0.0" />
    <PackageReference Include="Volo.Abp.Account.Web.IdentityServer" Version="4.0.0" />
    <PackageReference Include="Volo.Abp.Account.Application" Version="4.0.0" />
 ```



### 依赖包

- `Microsoft.Extensions.Hosting`





### 基本设置

`appsettings.json`

```json
{
  "App": {
    "SelfUrl": "https://localhost:8003",
    "CorsOrigins": "https://*.BookStore.com,http://localhost:8009,https://localhost:8008,https://localhost:8001"
  },
  "ConnectionStrings": {
    "Default": "Server=(LocalDb)\\MSSQLLocalDB;Database=BookStore;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Redis": {
    "Configuration": "127.0.0.1"
  },
  "StringEncryption": {
    "DefaultPassPhrase": "iIpMRCMOnSTU6lxK"
  },
  "Settings": {
    "Abp.Mailing.Smtp.Host": "127.0.0.1",
    "Abp.Mailing.Smtp.Port": "25",
    "Abp.Mailing.Smtp.UserName": "",
    "Abp.Mailing.Smtp.Password": "",
    "Abp.Mailing.Smtp.Domain": "",
    "Abp.Mailing.Smtp.EnableSsl": "false",
    "Abp.Mailing.Smtp.UseDefaultCredentials": "true",
    "Abp.Mailing.DefaultFromAddress": "noreply@abp.io",
    "Abp.Mailing.DefaultFromDisplayName": "ABP application"
  }
}

```

开发环境配置：

- Identity-Server：
  - https://localhost:8003

- Web网站：

  - http://localhost:8009
  - https://localhost:8008

- *.BookStore.HttpApi.Host 项目：

   https://localhost:8100

  

`appsettings.Production.json`

```C#
{
  "App": {
    "SelfUrl": "https://localhost:8103",
    "CorsOrigins": "https://*.BookStore.com,http://localhost:8109,https://localhost:81008,https://localhost:8100"
  },
  "ConnectionStrings": {
    "Default": "Server=.;Database=BookStore_Zto;Uid=sa;Password=123456;MultipleActiveResultSets=true"
  },
  "Redis": {
    "Configuration": "127.0.0.1"
  },
  "StringEncryption": {
    "DefaultPassPhrase": "iIpMRCMOnSTU6lxK"
  },
  "Settings": {
    "Abp.Mailing.Smtp.Host": "127.0.0.1",
    "Abp.Mailing.Smtp.Port": "25",
    "Abp.Mailing.Smtp.UserName": "",
    "Abp.Mailing.Smtp.Password": "",
    "Abp.Mailing.Smtp.Domain": "",
    "Abp.Mailing.Smtp.EnableSsl": "false",
    "Abp.Mailing.Smtp.UseDefaultCredentials": "true",
    "Abp.Mailing.DefaultFromAddress": "noreply@abp.io",
    "Abp.Mailing.DefaultFromDisplayName": "ABP application"
  }
}

```

- 生成环境配置：

  - Identity-Server：
    - https://localhost:8103

  - Web网站：

    - http://localhost:8109
    - https://localhost:8108

  - *.BookStore.HttpApi.Host 项目：

     https://localhost:8100

  

####  `Startup.cs`

```C#
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Zto.BookStore.IdentityServer;

namespace Zto.BookStore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<BookStoreIdentityServerModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}

```



#### `Program.cs`

```C#
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace Zto.BookStore
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
#if DEBUG
                .WriteTo.Async(c => c.Console())
#endif
                .CreateLogger();

            try
            {
                Log.Information("Starting Zto.BookStore.IdentityServer.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Zto.BookStore.IdentityServer terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseAutofac()
                .UseSerilog();
    }
}

```



### BookStoreIdentityServerModule

`BookStoreIdentityServerModule.cs`

```C#
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpAccountWebIdentityServerModule),
        typeof(AbpAccountApplicationModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(BookStoreEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAspNetCoreSerilogModule)
        )]
    public class BookStoreIdentityServerModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";
        
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //........
        }
        
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
          //........
        }
    }
```





# 2.`Authors`领域

这一部分在第一部分的搭建好基础框架的基础上，创建`Authors` 的相关业务

文本档可参见

[Authors: Domain layer](https://docs.abp.io/en/abp/latest/Tutorials/Part-6?UI=MVC&DB=EF)

（待续......）

