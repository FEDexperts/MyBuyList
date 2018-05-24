using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

using MyBuyList.Shared.Entities;

namespace MyBuyList.DataLayer.DataAdapters
{
    class AdminDA : BaseContextDataAdapter<MyBuyListDBContext>
    {
        #region Categories

        internal bool CheckDuplicateCategoryName(int categoryId, string categoryName)
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.Categories.Any(cat => cat.CategoryId != categoryId &&
                                                             cat.CategoryName.Trim() == categoryName.Trim());
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool CheckDuplicateMCategoryName(int categoryId, string categoryName)
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.MCategories.Any(cat => cat.MCategoryId != categoryId &&
                                                             cat.MCategoryName.Trim() == categoryName.Trim());
                }
                catch
                {
                    return false;
                }
            }
        }

        internal Category[] GetCategoriesList()
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.Categories.OrderBy(cat => cat.SortOrder);
                    foreach (Category item in list)
                    {
                        item.ParentCategories.Load();
                    }
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal MCategory[] GetMCategoriesList()
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.MCategories.OrderBy(mcat => mcat.SortOrder);
                   
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Category GetCategory(int categoryId)
        {
            using (DataContext)
            {
                try
                {
                    Category cat = DataContext.Categories.Single(c => c.CategoryId == categoryId);
                    cat.ParentCategory = DataContext.Categories.SingleOrDefault(c => c.CategoryId == cat.ParentCategoryId);
                    cat.AllowDelete = (cat.RecipeCategories.Count == 0);
                    if (cat.AllowDelete)
                    {
                        //check if this category has children categories
                        cat.AllowDelete = !DataContext.Categories.Any(c => c.ParentCategoryId == categoryId);
                    }
                    return cat;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal MCategory GetMenuCategory(int categoryId)
        {
            using (DataContext)
            {
                try
                {
                    MCategory cat = DataContext.MCategories.Single(c => c.MCategoryId == categoryId);
                    cat.ParentMCategory = DataContext.MCategories.SingleOrDefault(c => c.MCategoryId == cat.ParentMCategoryId);
                    cat.AllowDelete = (cat.MenuCategories.Count == 0);
                    if (cat.AllowDelete)
                    {
                        //check if this category has children categories
                        cat.AllowDelete = !DataContext.MCategories.Any(c => c.ParentMCategoryId == categoryId);
                    }
                    return cat;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool SaveCategory(Category category)
        {
            using (DataContext)
            {
                try
                {
                    if (!DataContext.Categories.Contains(category))
                    {
                        category.CreatedDate = DateTime.Now;
                        category.SortOrder = DataContext.Categories.Max(cat => cat.SortOrder) + 1;
                        DataContext.Categories.InsertOnSubmit(category);
                    }
                    else
                    {
                        string categoryName = category.CategoryName;
                        int? parentCategoryId = category.ParentCategoryId;
                        category = DataContext.Categories.Single(cat => cat.CategoryId == category.CategoryId);
                        category.CategoryName = categoryName;
                        category.ParentCategoryId = parentCategoryId;
                    }

                    category.ModifiedDate = DateTime.Now;
                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool SaveCategory(int categoryId, string categoryName, int? parentCategoryId)
        {
            using (DataContext)
            {
                try
                {

                    Category category = DataContext.Categories.SingleOrDefault(cat => cat.CategoryId == categoryId);
                    if (category == null)
                    {
                        category = new Category();
                        category.CategoryName = categoryName;
                        category.ParentCategoryId = parentCategoryId;
                        category.CreatedDate = DateTime.Now;
                        category.ModifiedDate = DateTime.Now;
                        category.SortOrder = DataContext.Categories.Max(cat => cat.SortOrder) + 1;
                        DataContext.Categories.InsertOnSubmit(category);
                    }
                    else
                    {
                        category.CategoryName = categoryName;
                        category.ParentCategoryId = parentCategoryId;
                        category.ModifiedDate = DateTime.Now;
                    }

                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool SaveMenuCategory(int categoryId, string categoryName, int? parentCategoryId)
        {
            using (DataContext)
            {
                try
                {

                    MCategory category = DataContext.MCategories.SingleOrDefault(cat => cat.MCategoryId == categoryId);
                    if (category == null)
                    {
                        category = new MCategory();
                        category.MCategoryName = categoryName;
                        category.ParentMCategoryId = parentCategoryId;
                        category.CreatedDate = DateTime.Now;
                        category.ModifiedDate = DateTime.Now;
                        category.SortOrder = DataContext.Categories.Max(cat => cat.SortOrder) + 1;
                        DataContext.MCategories.InsertOnSubmit(category);
                    }
                    else
                    {
                        category.MCategoryName = categoryName;
                        category.ParentMCategoryId = parentCategoryId;
                        category.ModifiedDate = DateTime.Now;
                    }

                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool DeleteCategory(int categoryId)
        {
            using (DataContext)
            {
                try
                {
                    Category category = DataContext.Categories.Single(cat => cat.CategoryId == categoryId);
                    DataContext.Categories.DeleteOnSubmit(category);
                    DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool DeleteMenuCategory(int categoryId)
        {
            using (DataContext)
            {
                try
                {
                    MCategory category = DataContext.MCategories.Single(cat => cat.MCategoryId == categoryId);
                    DataContext.MCategories.DeleteOnSubmit(category);
                    DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion Categories

        #region FoodCategories

        internal bool CheckDuplicateFoodCategoryName(int FoodCategoryId, string FoodCategoryName)
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.FoodCategories.Any(cat => cat.FoodCategoryId != FoodCategoryId &&
                                                             cat.FoodCategoryName.Trim() == FoodCategoryName.Trim());
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool CheckDuplicateFoodName(int FoodId, string FoodName)
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.Foods.Any(f => f.FoodId != FoodId &&
                                                             f.FoodName.Trim() == FoodName.Trim());
                }
                catch
                {
                    return false;
                }
            }
        }

        internal FoodCategory[] GetFoodCategoriesList()
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.FoodCategories.OrderBy(fcat => fcat.SortOrder);
                    foreach (FoodCategory item in list)
                    {
                        item.FoodCategories.Load();
                    }
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal FoodCategory GetFoodCategory(int FoodCategoryId)
        {
            using (DataContext)
            {
                try
                {
                    FoodCategory cat = DataContext.FoodCategories.Single(c => c.FoodCategoryId == FoodCategoryId);
                    cat.ParentFoodCategory = DataContext.FoodCategories.SingleOrDefault(c => c.FoodCategoryId == cat.ParentCategoryId);
                    cat.AllowDelete = (cat.FoodCategories.Count == 0);
                    if (cat.AllowDelete)
                    {
                        //check if this FoodCategory has children FoodCategories
                        cat.AllowDelete = !DataContext.FoodCategories.Any(c => c.ParentCategoryId == FoodCategoryId);
                    }
                    return cat;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool SaveFoodCategory(FoodCategory FoodCategory)
        {
            using (DataContext)
            {
                try
                {
                    if (!DataContext.FoodCategories.Contains(FoodCategory))
                    {
                        FoodCategory.CreatedDate = DateTime.Now;
                        FoodCategory.SortOrder = DataContext.FoodCategories.Max(cat => cat.SortOrder) + 1;
                        DataContext.FoodCategories.InsertOnSubmit(FoodCategory);
                    }
                    else
                    {
                        string FoodCategoryName = FoodCategory.FoodCategoryName;
                        int? ParentCategoryId = FoodCategory.ParentCategoryId;
                        FoodCategory = DataContext.FoodCategories.Single(cat => cat.FoodCategoryId == FoodCategory.FoodCategoryId);
                        FoodCategory.FoodCategoryName = FoodCategoryName;
                        FoodCategory.ParentCategoryId = ParentCategoryId;
                    }

                    FoodCategory.ModifiedDate = DateTime.Now;
                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }



        internal bool SaveFoodCategory(int FoodCategoryId, string FoodCategoryName, int? ParentCategoryId)
        {
            using (DataContext)
            {
                try
                {

                    FoodCategory FoodCategory = DataContext.FoodCategories.SingleOrDefault(cat => cat.FoodCategoryId == FoodCategoryId);
                    if (FoodCategory == null)
                    {
                        FoodCategory = new FoodCategory();
                        FoodCategory.FoodCategoryName = FoodCategoryName;
                        FoodCategory.ParentCategoryId = ParentCategoryId;
                        FoodCategory.CreatedDate = DateTime.Now;
                        FoodCategory.ModifiedDate = DateTime.Now;
                        FoodCategory.SortOrder = DataContext.FoodCategories.Max(cat => cat.SortOrder) + 1;
                        DataContext.FoodCategories.InsertOnSubmit(FoodCategory);
                    }
                    else
                    {
                        FoodCategory.FoodCategoryName = FoodCategoryName;
                        FoodCategory.ParentCategoryId = ParentCategoryId;
                        FoodCategory.ModifiedDate = DateTime.Now;
                    }

                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool DeleteFoodCategory(int FoodCategoryId)
        {
            using (DataContext)
            {
                try
                {
                    FoodCategory FoodCategory = DataContext.FoodCategories.Single(cat => cat.FoodCategoryId == FoodCategoryId);
                    DataContext.FoodCategories.DeleteOnSubmit(FoodCategory);
                    DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion FoodCategories

        #region Foods

        internal Food[] GetFoodsList()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            dlo.LoadWith<Food>(f => f.Ingredients);
            DataContext.LoadOptions = dlo;
            var list = DataContext.Foods.OrderBy(f => f.FoodName);
            return list.ToArray();
        }

        internal Food GetFood(int foodId)
        {
            using (DataContext)
            {
                Food food = DataContext.Foods.Single(f => f.FoodId == foodId);
                return food;
            }
        }

        internal Food GetFood(string name)
        {
            using (DataContext)
            {
                try
                {
                    Food food = DataContext.Foods.SingleOrDefault(f => f.FoodName.Trim() == name);
                    return food;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool SaveFood(Food food)
        {
            using (DataContext)
            {
                try
                {
                    if (!DataContext.Foods.Contains(food))
                    {
                        food.CreatedDate = DateTime.Now;
                        food.IsTemporary = true;
                        DataContext.Foods.InsertOnSubmit(food);
                    }
                    else
                    {
                        food.IsTemporary = false;
                        DataContext.Foods.Attach(food);
                        DataContext.Refresh(RefreshMode.KeepCurrentValues, food);
                    }

                    food.ModifiedDate = DateTime.Now;
                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion Foods

        #region ShopDepartments

        internal bool CheckDuplicateShopDepartmentName(int departmentId, string departmentName)
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.ShopDepartments.Any(sd => sd.ShopDepartmentId != departmentId &&
                                                                 sd.ShopDepartmentName.Trim() == departmentName.Trim());
                }
                catch
                {
                    return false;
                }
            }
        }

        internal ShopDepartment GetShopDepartment(int departmentId)
        {
            using (DataContext)
            {
                try
                {
                    ShopDepartment dep = DataContext.ShopDepartments.Single(sd => sd.ShopDepartmentId == departmentId);
                    return dep;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal ShopDepartment[] GetShopDepartmentsList()
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<ShopDepartment>(sd => sd.FoodCategories);
                    dlo.LoadWith<FoodCategory>(fc => fc.Foods);

                    DataContext.LoadOptions = dlo;

                    var list = DataContext.ShopDepartments.OrderBy(sd => sd.SortOrder);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool DeleteShopDepartment(int shopDepartmentId)
        {
            using (DataContext)
            {
                DataLoadOptions dlo = new DataLoadOptions();
                dlo.LoadWith<ShopDepartment>(sd => sd.FoodCategories);


                DataContext.LoadOptions = dlo;
                try
                {
                    ShopDepartment department = DataContext.ShopDepartments.Single(sd => sd.ShopDepartmentId == shopDepartmentId);
                    FoodCategory[] depFoodCat = department.FoodCategories.ToArray<FoodCategory>();
                    foreach (FoodCategory curr in depFoodCat)
                    {
                        this.DeleteFoodCategory(curr.FoodCategoryId);
                    }

                    DataContext.ShopDepartments.DeleteOnSubmit(department);
                    DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool SaveShopDepartment(ShopDepartment department)
        {
            using (DataContext)
            {
                try
                {
                    if (!DataContext.ShopDepartments.Contains(department))
                    {
                        department.CreatedDate = DateTime.Now;
                        department.SortOrder = DataContext.ShopDepartments.Max(d => d.SortOrder) + 1;
                        DataContext.ShopDepartments.InsertOnSubmit(department);
                    }
                    else
                    {
                        string departmentName = department.ShopDepartmentName;
                        department = DataContext.ShopDepartments.Single(d => d.ShopDepartmentId == department.ShopDepartmentId);
                        department.ShopDepartmentName = departmentName;
                    }

                    department.ModifiedDate = DateTime.Now;
                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool ReorderShopDepartments(ShopDepartment[] arr)
        {
            using (DataContext)
            {
                try
                {
                    foreach (ShopDepartment item in arr)
                    {
                        ShopDepartment dep = DataContext.ShopDepartments.Single(sd => sd.ShopDepartmentId == item.ShopDepartmentId);
                        dep.SortOrder = item.SortOrder;
                        dep.ModifiedDate = DateTime.Now;
                        DataContext.SubmitChanges();
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        #endregion ShopDepartments

        #region GeneralItems

        internal bool CheckDuplicateGeneralItemName(int ItemId, string ItemName)
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.GeneralItems.Any(sd => sd.GeneralItemId != ItemId &&
                                                                 sd.GeneralItemName.Trim() == ItemName.Trim());
                }
                catch
                {
                    return false;
                }
            }
        }

        internal GeneralItem GetGeneralItem(int ItemId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<GeneralItem>(sd => sd.ShoppingListAdditionalItems);
                    DataContext.LoadOptions = dlo;

                    GeneralItem dep = DataContext.GeneralItems.Single(sd => sd.GeneralItemId == ItemId);
                    return dep;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal GeneralItem[] GetGeneralItemsList()
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<GeneralItem>(sd => sd.ShoppingListAdditionalItems);
                    DataContext.LoadOptions = dlo;

                    var list = DataContext.GeneralItems.OrderBy(sd => sd.SortOrder);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool DeleteGeneralItem(int GeneralItemId)
        {
            using (DataContext)
            {
                try
                {
                    GeneralItem Item = DataContext.GeneralItems.Single(sd => sd.GeneralItemId == GeneralItemId);
                    DataContext.GeneralItems.DeleteOnSubmit(Item);
                    DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool SaveGeneralItem(GeneralItem Item)
        {
            using (DataContext)
            {
                try
                {
                    if (!DataContext.GeneralItems.Contains(Item))
                    {
                        // Item.CreatedDate = DateTime.Now;
                        Item.SortOrder = DataContext.GeneralItems.Max(d => d.SortOrder) + 1;
                        DataContext.GeneralItems.InsertOnSubmit(Item);
                    }
                    else
                    {
                        string ItemName = Item.GeneralItemName;
                        Item = DataContext.GeneralItems.Single(d => d.GeneralItemId == Item.GeneralItemId);
                        Item.GeneralItemName = ItemName;
                    }

                    //Item.ModifiedDate = DateTime.Now;
                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool ReorderGeneralItems(GeneralItem[] arr)
        {
            using (DataContext)
            {
                try
                {
                    foreach (GeneralItem item in arr)
                    {
                        GeneralItem dep = DataContext.GeneralItems.Single(sd => sd.GeneralItemId == item.GeneralItemId);
                        dep.SortOrder = item.SortOrder;
                        //dep.ModifiedDate = DateTime.Now;
                        DataContext.SubmitChanges();
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        #endregion GeneralItems

        #region MeasurementUnits

        internal bool CheckDuplicateMeasurementUnitName(int unitId, string unitName)
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.MeasurementUnits.Any(mu => mu.UnitId != unitId &&
                                                                  mu.UnitName.Trim() == unitName.Trim());
                }
                catch
                {
                    return false;
                }
            }
        }

        internal MeasurementUnit GetMeasurementUnit(int unitId)
        {
            using (DataContext)
            {
                try
                {
                    MeasurementUnit unit = DataContext.MeasurementUnits.Single(mu => mu.UnitId == unitId);
                    return unit;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal MeasurementUnit[] GetMeasurementUnitsList()
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.MeasurementUnits.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool DeleteMeasurementUnit(int unitId)
        {
            using (DataContext)
            {
                try
                {
                    MeasurementUnit unit = DataContext.MeasurementUnits.Single(mu => mu.UnitId == unitId);
                    DataContext.MeasurementUnits.DeleteOnSubmit(unit);
                    DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool SaveMeasurementUnit(MeasurementUnit unit)
        {
            using (DataContext)
            {
                try
                {
                    if (!DataContext.MeasurementUnits.Contains(unit))
                    {
                        unit.CreatedDate = DateTime.Now;
                        unit.SortOrder = DataContext.MeasurementUnits.Max(mu => mu.SortOrder) + 1;
                        DataContext.MeasurementUnits.InsertOnSubmit(unit);
                    }
                    else
                    {
                        string unitName = unit.UnitName;
                        unit = DataContext.MeasurementUnits.Single(mu => mu.UnitId == unit.UnitId);
                        unit.UnitName = unitName;
                    }

                    unit.ModifiedDate = DateTime.Now;
                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool ReorderMeasurementUnits(MeasurementUnit[] arr)
        {
            using (DataContext)
            {
                try
                {
                    foreach (MeasurementUnit item in arr)
                    {
                        MeasurementUnit unit = DataContext.MeasurementUnits.Single(mu => mu.UnitId == item.UnitId);
                        unit.SortOrder = item.SortOrder;
                        unit.ModifiedDate = DateTime.Now;
                        DataContext.SubmitChanges();
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion MeasurementUnits

        internal bool SaveMeasurementUnitsConvert(MeasurementUnitsConvert convert)
        {
            using (DataContext)
            {
                try
                {
                    if (!DataContext.MeasurementUnitsConverts.Contains(convert))
                    {
                        convert.CreatedDate = DateTime.Now;
                        convert.ModifiedDate = DateTime.Now;
                        DataContext.MeasurementUnitsConverts.InsertOnSubmit(convert);
                    }
                    else
                    {
                        MeasurementUnitsConvert itemToSave = DataContext.MeasurementUnitsConverts.Single(muc => muc.ConvertId == convert.ConvertId);
                        itemToSave.FoodId = convert.FoodId;
                        itemToSave.FromUnitId = convert.FromUnitId;
                        itemToSave.FromQuantity = convert.FromQuantity;
                        itemToSave.ToUnitId = convert.ToUnitId;
                        itemToSave.ToQuantity = convert.ToQuantity;
                        itemToSave.ModifiedDate = DateTime.Now;
                    }

                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal MeasurementUnitsConvert[] GetMeasurementUnitsConvertList()
        {
            using (DataContext)
            {
                DataLoadOptions dlo = new DataLoadOptions();
                //dlo.LoadWith<MeasurementUnitsConvert>(m => m.Food);
                //dlo.LoadWith<MeasurementUnitsConvert>(m => m.FromMeasurementUnit);
                //dlo.LoadWith<MeasurementUnitsConvert>(m => m.ToMeasurementUnit);

                DataContext.LoadOptions = dlo;

                try
                {
                    var list = DataContext.MeasurementUnitsConverts.OrderBy(sd => sd.SortOrder);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal MeasurementUnitsConvert GetMeasurementUnitsConvert(int id)
        {
            using (DataContext)
            {
                DataLoadOptions dlo = new DataLoadOptions();
                //dlo.LoadWith<MeasurementUnitsConvert>(m => m.Food);
                //dlo.LoadWith<MeasurementUnitsConvert>(m => m.FromMeasurementUnit);
                //dlo.LoadWith<MeasurementUnitsConvert>(m => m.ToMeasurementUnit);
                DataContext.LoadOptions = dlo;

                try
                {
                    MeasurementUnitsConvert unit = DataContext.MeasurementUnitsConverts.SingleOrDefault(mu => mu.ConvertId == id);

                    return unit;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal User GetUserByName(string Name)
        {
            using (DataContext)
            {
                //DataLoadOptions dlo = new DataLoadOptions();
                //dlo.LoadWith<User>(u => u.Foods);
                //dlo.LoadWith<User>(u => u.Foods1);
                //dlo.LoadWith<User>(u => u.Recipes);
                //dlo.LoadWith<User>(u => u.UserFavoriteRecipes);
                //dlo.LoadWith<User>(u => u.UserType);
                //dlo.LoadWith<User>(u => u.Menus);
                //DataContext.LoadOptions = dlo;

                try
                {

                    User user = DataContext.Users.SingleOrDefault(u => u.Name.Trim() == Name.Trim());

                    return user;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal int GetNextTempUser(int anonymous)
        {
            using (DataContext)
            {
                try
                {
                    //int anonymous = int.Parse(ConfigurationManager.AppSettings["anonymous"]);
                    int? lastTempUser = (from m in DataContext.Menus
                                         where m.UserId == anonymous
                                         select m.TempUserId)
                                        .Max();
                    if (lastTempUser.HasValue)
                    {
                        return lastTempUser.Value + 1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch
                {
                    return 1;
                }
            }
        }

        internal int GetUsersNum()
        {
            using (DataContext)
            {
                //DataLoadOptions dlo = new DataLoadOptions();
                //dlo.LoadWith<User>(u => u.Foods);
                //dlo.LoadWith<User>(u => u.Foods1);
                //dlo.LoadWith<User>(u => u.Recipes);
                //dlo.LoadWith<User>(u => u.UserFavoriteRecipes);
                //dlo.LoadWith<User>(u => u.UserType);
                //dlo.LoadWith<User>(u => u.Menus);
                //DataContext.LoadOptions = dlo;

                try
                {
                    return DataContext.Users.Count();
                }
                catch
                {
                    return -1;
                }
            }
        }

        internal User GetUser(int Id)
        {
            using (DataContext)
            {
                //DataLoadOptions dlo = new DataLoadOptions();
                //dlo.LoadWith<User>(u => u.Foods);
                //dlo.LoadWith<User>(u => u.Foods1);
                //dlo.LoadWith<User>(u => u.Recipes);
                //dlo.LoadWith<User>(u => u.UserFavoriteRecipes);
                //dlo.LoadWith<User>(u => u.UserType);
                //dlo.LoadWith<User>(u => u.Menus);
                //DataContext.LoadOptions = dlo;

                try
                {
                    User user = DataContext.Users.SingleOrDefault(u => u.UserId == Id);

                    return user;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal User GetUser(string userName, string password)
        {
            //using (DataContext)
            //{
                User user = DataContext.Users.SingleOrDefault(u => u.Name == userName && u.Password == password);
                return user;
            //}
        }

        internal User GetUserEx(int Id)
        {
            using (DataContext)
            {
                DataLoadOptions dlo = new DataLoadOptions();
                dlo.LoadWith<User>(u => u.UserFavoriteRecipes);
                dlo.LoadWith<User>(u => u.UserFavoriteMenus);
                dlo.LoadWith<UserFavoriteMenu>(ufm => ufm.Menu);
                dlo.LoadWith<UserFavoriteRecipe>(ufr => ufr.Recipe); 
                DataContext.LoadOptions = dlo;

                try
                {
                    User user = DataContext.Users.SingleOrDefault(u => u.UserId == Id);

                    return user;
                }
                catch
                {
                    return null;
                }
            }
        }


        internal bool SaveUser(User userToSave)
        {
            using (DataContext)
            {
                User user = DataContext.Users.SingleOrDefault(p => p.Name == userToSave.Name && p.Password == userToSave.Password);
                if (user == null)
                {
                    DateTime currentDate = DateTime.Now;
                    userToSave.UserId = 0;

                    DataContext.Users.InsertOnSubmit(userToSave);
                }
                else
                {
                    User update = DataContext.Users.Single(u => u.UserId == userToSave.UserId);
                    update.DisplayName = userToSave.DisplayName;
                    update.Email = userToSave.Email;
                    update.FirstName = userToSave.FirstName;
                    update.LastName = userToSave.LastName;
                    update.AgreeToMail = userToSave.AgreeToMail;
                    update.Name = userToSave.Name;
                    update.Password = userToSave.Password;
                }

                DataContext.SubmitChanges();
                return true;
            }
        }

        internal FoodCategory GetFoodCategoryByName(string name)
        {
            using (DataContext)
            {
                try
                {
                    FoodCategory cat = DataContext.FoodCategories.Single(c => c.FoodCategoryName == name.Trim());
                    cat.ParentFoodCategory = DataContext.FoodCategories.SingleOrDefault(c => c.FoodCategoryId == cat.ParentCategoryId);
                    cat.AllowDelete = (cat.FoodCategories.Count == 0);
                    if (cat.AllowDelete)
                    {
                        //check if this FoodCategory has children FoodCategories
                        cat.AllowDelete = !DataContext.FoodCategories.Any(c => c.ParentCategoryId == cat.FoodCategoryId);
                    }
                    return cat;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal void ReplaceUserIds(int sourceUserId, int targetUserId)
        {
            using (DataContext)
            {
                // foods
                var foods = from f in DataContext.Foods
                            where f.CreatedBy == sourceUserId
                            select f;

                foreach (var food in foods)
                {
                    food.CreatedBy = targetUserId;
                }

                var foods2 = from f in DataContext.Foods
                             where f.ModifiedBy == sourceUserId
                             select f;

                foreach (var food in foods2)
                {
                    food.ModifiedBy = targetUserId;
                }

                // menus
                var menus = from m in DataContext.Menus
                            where m.UserId == sourceUserId
                            select m;

                foreach (var menu in menus)
                {
                    menu.UserId = targetUserId;
                }

                // recipes
                var recipes = from r in DataContext.Recipes
                              where r.UserId == sourceUserId
                              select r;

                foreach (var recipe in recipes)
                {
                    recipe.UserId = targetUserId;
                }

                // favorites
                var favs = from f in DataContext.UserFavoriteRecipes
                           where f.UserId == sourceUserId
                           select f;

                foreach (var fav in favs)
                {
                    UserFavoriteRecipe fav2 = new UserFavoriteRecipe();
                    fav2.UserId = targetUserId;
                    fav2.RecipeId = fav.RecipeId;
                    DataContext.UserFavoriteRecipes.InsertOnSubmit(fav2);
                    DataContext.UserFavoriteRecipes.DeleteOnSubmit(fav);
                }

                DataContext.SubmitChanges();
            }
        }

        internal bool DeleteFood(int foodId)
        {
            using (DataContext)
            {
                try
                {
                    Food Item = DataContext.Foods.Single(f => f.FoodId == foodId);
                    DataContext.Foods.DeleteOnSubmit(Item);
                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal Article GetArticleById(int articleId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Article>(art => art.ArticleId);
                    DataContext.LoadOptions = dlo;

                    Article article = DataContext.Articles.SingleOrDefault(a => a.ArticleId == articleId );

                    return article;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Article[] GetArticlesList()
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Article>(art => art.ArticleId);
                    DataContext.LoadOptions = dlo;

                    var list = from ar in DataContext.Articles.Where(ar => ar.ArticleId != 6 )
                               select ar;

                    return list.ToArray();                    
                }
                catch
                {
                    return null;
                }
            }

        }

        internal bool CreateOrUpdateArticle(int id, string title, string abs, string body, string publisher, out int returnedId )
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Article>(art => art.ArticleId);
                    DataContext.LoadOptions = dlo;

                    Article article = DataContext.Articles.SingleOrDefault(art => art.ArticleId == id);

                    bool isNew = false;

                    if (article == null)
                    {
                        article = new Article();
                        article.CreatedDate = DateTime.Now;
                        isNew = true;
                    }

                    article.Title = title;
                    article.Abstract = abs;
                    article.Body = body;
                    article.Publisher = publisher;
                    article.ModifiedDate = DateTime.Now;

                    if (isNew)
                    {
                        DataContext.Articles.InsertOnSubmit(article);
                    }

                    DataContext.SubmitChanges();

                    returnedId = article.ArticleId;

                    return true;
                }
                catch
                {
                    returnedId = -1;
                    return false;
                }
            }

        }
    }
}
