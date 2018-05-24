using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyBuyList.DataLayer;
using MyBuyList.Shared.Entities;

namespace MyBuyList.BusinessLayer.Managers
{

    public enum MenuTypeEnum : int
    {
        OneMeal = 1,
        Weekly = 2,
        ManyWeeks = 3,
        QuickMenu = 4
    }  
    
    [Serializable]
    public class MealDayOfWeek
    {
        public int DayId { get; set; }
        public string DayName { get; set; }

        public MealDayOfWeek(int dayId, string dayName)
        {
            this.DayId = dayId;
            this.DayName = dayName;
        }
    }

    class MealsManager
    {
        internal MealRecipe[] GetMealsWeeklyList(int menuId, int startDayIndex, int endDayIndex)
        {
            
            return DataFacade.Instance.GetMealsWeeklyList(menuId, startDayIndex, endDayIndex);
        }

        internal CourseType[] GetCourseTypes()
        {
            return DataFacade.Instance.GetCourseTypes();
        }

        internal MealType[] GetMealTypes()
        {
            return DataFacade.Instance.GetMealTypes();
        }

        internal Meal[] GetMealsList(int menuId)
        {
            return DataFacade.Instance.GetMealsList(menuId);
        }

        internal Meal GetMeal(int mealId)
        {
            return DataFacade.Instance.GetMeal(mealId);
        }

        internal Meal GetMeal(int menuId, int courseTypeId)
        {
            return DataFacade.Instance.GetMeal(menuId, courseTypeId);
        }

        internal Meal GetMeal(int menuId, int dayIndex, int mealTypeId)
        {
            return DataFacade.Instance.GetMeal(menuId, dayIndex, mealTypeId);
        }

        internal bool SaveMeal(int menuId, int courseTypeId, int? diners)
        {
            return DataFacade.Instance.SaveMeal(menuId, courseTypeId, diners);
        }

        internal bool SaveMeal(int menuId, int dayIndex, int mealTypeId, int? diners)
        {
            return DataFacade.Instance.SaveMeal(menuId, dayIndex, mealTypeId, diners);
        }

        internal MealRecipe[] GetMealRecipesList(int menuId, int dayIndex, int mealTypeId)
        {
            return DataFacade.Instance.GetMealRecipesList(menuId, dayIndex, mealTypeId);
        }

        internal bool CreateQuickListMealRecipes(int menuId)
        {
            return DataFacade.Instance.CreateQuickListMealRecipes(menuId);
        }

        internal bool AddMealRecipe(int menuId, int courseTypeId, int recipeId, out int mealId)
        {
            return DataFacade.Instance.AddMealRecipe(menuId, courseTypeId, recipeId, out mealId);
        }

        internal bool AddMealRecipe(int menuId, int dayIndex, int mealTypeId, int recipeId, out int mealId)
        {
            return DataFacade.Instance.AddMealRecipe(menuId, dayIndex, mealTypeId, recipeId, out mealId);
        }

        internal bool RemoveMealRecipe(int mealId, int recipeId)
        {
            return DataFacade.Instance.RemoveMealRecipe(mealId, recipeId);
        }

        internal MealRecipe GetMealRecipe(int mealId, int recipeId)
        {
            return DataFacade.Instance.GetMealRecipe(mealId, recipeId);
        }

        internal bool ClearAllMeals(int menuId)
        {
            return DataFacade.Instance.ClearAllMeals(menuId);
        }

        internal bool SaveMealRecipe(int mealId, int recipeId, int servings)
        {
            return DataFacade.Instance.SaveMealRecipe(mealId, recipeId, servings);
        }

        internal MealRecipe[] GetMealRecipes(int mealId)
        {
            return DataFacade.Instance.GetMealRecipes(mealId);
        }
    }
}
