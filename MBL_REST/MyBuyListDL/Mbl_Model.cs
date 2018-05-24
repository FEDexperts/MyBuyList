namespace MyBuyListDL
{
    using System.Data.Entity;

    public partial class Mbl_Model : DbContext
    {
        public Mbl_Model()
            : base("name=Mbl_Model")
        {
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CourseTypes> CourseTypes { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<FoodCategories> FoodCategories { get; set; }
        public virtual DbSet<GeneralItems> GeneralItems { get; set; }
        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<MBLSettings> MBLSettings { get; set; }
        public virtual DbSet<MCategories> MCategories { get; set; }
        public virtual DbSet<MealRecipes> MealRecipes { get; set; }
        public virtual DbSet<Meals> Meals { get; set; }
        public virtual DbSet<MealTypes> MealTypes { get; set; }
        public virtual DbSet<MeasurementUnits> MeasurementUnits { get; set; }
        public virtual DbSet<MeasurementUnitsConverts> MeasurementUnitsConverts { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<MenuTypes> MenuTypes { get; set; }
        public virtual DbSet<MissingListDetails> MissingListDetails { get; set; }
        public virtual DbSet<MissingLists> MissingLists { get; set; }
        public virtual DbSet<NutCategories> NutCategories { get; set; }
        public virtual DbSet<NutItems> NutItems { get; set; }
        public virtual DbSet<NutValues> NutValues { get; set; }
        public virtual DbSet<ReciepList> ReciepList { get; set; }
        public virtual DbSet<ReciepListDetails> ReciepListDetails { get; set; }
        public virtual DbSet<Recipes> Recipes { get; set; }
        public virtual DbSet<RecipesInShoppingList> RecipesInShoppingList { get; set; }
        public virtual DbSet<SavedList> SavedList { get; set; }
        public virtual DbSet<SavedListDetails> SavedListDetails { get; set; }
        public virtual DbSet<ShopDepartments> ShopDepartments { get; set; }
        public virtual DbSet<ShoppingListAdditionalItems> ShoppingListAdditionalItems { get; set; }
        public virtual DbSet<SiteUsers> SiteUsers { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserShoppingList> UserShoppingList { get; set; }
        public virtual DbSet<UserTypes> UserTypes { get; set; }
        public virtual DbSet<RecipesFavorits> RecipesFavorits { get; set; }
        public virtual DbSet<ShoppingLists> ShoppingLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>()
                .HasMany(e => e.Categories1)
                .WithOptional(e => e.Categories2)
                .HasForeignKey(e => e.ParentCategoryId);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.Recipes)
                .WithMany(e => e.Categories)
                .Map(m => m.ToTable("RecipeCategories").MapLeftKey("CategoryId").MapRightKey("RecipeId"));

            modelBuilder.Entity<Food>()
                .HasMany(e => e.NutValues)
                .WithRequired(e => e.Food)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Food>()
                .HasMany(e => e.Ingredients)
                .WithRequired(e => e.Food)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Food>()
                .HasMany(e => e.MeasurementUnitsConverts)
                .WithRequired(e => e.Food)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Food>()
                .HasMany(e => e.MissingListDetails)
                .WithRequired(e => e.Food)
                .HasForeignKey(e => e.FOOD_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Food>()
                .HasMany(e => e.SavedListDetails)
                .WithRequired(e => e.Food)
                .HasForeignKey(e => e.FOOD_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FoodCategories>()
                .HasMany(e => e.Food)
                .WithRequired(e => e.FoodCategories)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FoodCategories>()
                .HasMany(e => e.FoodCategories1)
                .WithOptional(e => e.FoodCategories2)
                .HasForeignKey(e => e.ParentCategoryId);

            modelBuilder.Entity<Log>()
                .Property(e => e.Thread)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Level)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Logger)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Exception)
                .IsUnicode(false);

            modelBuilder.Entity<MCategories>()
                .HasMany(e => e.MCategories1)
                .WithOptional(e => e.MCategories2)
                .HasForeignKey(e => e.ParentMCategoryId);

            modelBuilder.Entity<MCategories>()
                .HasMany(e => e.Menus)
                .WithMany(e => e.MCategories)
                .Map(m => m.ToTable("MenuCategories").MapLeftKey("MCategoryId").MapRightKey("MenuId"));

            modelBuilder.Entity<MealTypes>()
                .HasMany(e => e.Meals)
                .WithRequired(e => e.MealTypes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MeasurementUnits>()
                .HasMany(e => e.Ingredients)
                .WithRequired(e => e.MeasurementUnits)
                .HasForeignKey(e => e.MeasurementUnitId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MeasurementUnits>()
                .HasMany(e => e.MeasurementUnitsConverts)
                .WithRequired(e => e.MeasurementUnits)
                .HasForeignKey(e => e.FromUnitId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MeasurementUnits>()
                .HasMany(e => e.MeasurementUnitsConverts1)
                .WithRequired(e => e.MeasurementUnits1)
                .HasForeignKey(e => e.ToUnitId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MeasurementUnits>()
                .HasMany(e => e.MissingListDetails)
                .WithRequired(e => e.MeasurementUnits)
                .HasForeignKey(e => e.MEASUREMENT_UNIT_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MeasurementUnits>()
                .HasMany(e => e.SavedListDetails)
                .WithRequired(e => e.MeasurementUnits)
                .HasForeignKey(e => e.MEASUREMENT_UNIT_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menus>()
                .HasMany(e => e.Recipes)
                .WithMany(e => e.Menus)
                .Map(m => m.ToTable("MenuRecipes").MapLeftKey("MenuId").MapRightKey("RecipeId"));

            modelBuilder.Entity<Menus>()
                .HasMany(e => e.Users1)
                .WithMany(e => e.Menus1)
                .Map(m => m.ToTable("MenusInShoppingList").MapLeftKey("MenuId").MapRightKey("UserId"));

            modelBuilder.Entity<Menus>()
                .HasMany(e => e.Users2)
                .WithMany(e => e.Menus2)
                .Map(m => m.ToTable("UserFavoriteMenus").MapLeftKey("MenuId").MapRightKey("UserId"));

            modelBuilder.Entity<MenuTypes>()
                .HasMany(e => e.Menus)
                .WithRequired(e => e.MenuTypes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MissingListDetails>()
                .Property(e => e.QUANTITY)
                .HasPrecision(5, 2);

            modelBuilder.Entity<MissingLists>()
                .HasMany(e => e.MissingListDetails)
                .WithRequired(e => e.MissingLists)
                .HasForeignKey(e => e.LIST_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NutCategories>()
                .HasMany(e => e.NutItems)
                .WithRequired(e => e.NutCategories)
                .HasForeignKey(e => e.NutCategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NutItems>()
                .HasMany(e => e.NutValues)
                .WithRequired(e => e.NutItems)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReciepList>()
                .HasMany(e => e.ReciepListDetails)
                .WithRequired(e => e.ReciepList)
                .HasForeignKey(e => e.LIST_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReciepList>()
                .HasMany(e => e.Recipes)
                .WithMany(e => e.ReciepList)
                .Map(m => m.ToTable("GeneralListRecieps").MapLeftKey("LIST_ID").MapRightKey("RECIEP_ID"));

            modelBuilder.Entity<Recipes>()
                .HasMany(e => e.Ingredients)
                .WithRequired(e => e.Recipes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Recipes>()
                .HasMany(e => e.MealRecipes)
                .WithRequired(e => e.Recipes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Recipes>()
                .HasMany(e => e.RecipesInShoppingList)
                .WithRequired(e => e.Recipes)
                .HasForeignKey(e => e.RECIPE_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Recipes>()
                .HasMany(e => e.Users1)
                .WithMany(e => e.Recipes1)
                .Map(m => m.ToTable("UserFavoriteRecipes").MapLeftKey("RecipeId").MapRightKey("UserId"));

            modelBuilder.Entity<SavedList>()
                .HasMany(e => e.SavedListDetails)
                .WithRequired(e => e.SavedList)
                .HasForeignKey(e => e.LIST_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SavedListDetails>()
                .Property(e => e.QUANTITY)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ShopDepartments>()
                .HasMany(e => e.FoodCategories)
                .WithRequired(e => e.ShopDepartments)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Food)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Food1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.ModifiedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Menus)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.MissingLists)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.CREATED_BY)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Recipes)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.RecipesInShoppingList)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.USER_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasOptional(e => e.ShoppingLists)
                .WithRequired(e => e.Users);

            modelBuilder.Entity<UserShoppingList>()
                .Property(e => e.QUANTITY)
                .HasPrecision(10, 2);

            modelBuilder.Entity<UserTypes>()
                .HasMany(e => e.SiteUsers)
                .WithRequired(e => e.UserTypes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTypes>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.UserTypes)
                .WillCascadeOnDelete(false);
        }
    }
}
