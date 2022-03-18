using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.Model
{
    internal static class RecipeBookParser
    {
        public static string RecipeBookPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"SmartPotato\RecipeBook.json");

        public static List<Recipe> ReadRecipeBook()
        {
            if (!File.Exists(RecipeBookPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(RecipeBookPath)!);
                File.Create(RecipeBookPath).Close();
            }
            string recipes = File.ReadAllText(RecipeBookPath);
            return DeserializeRecipes(recipes);
        }

        public static void UpdateRecipeBook(List<Recipe> recipes)
        {
            string updatedRecipes = SerializeRecipes(recipes);
            File.WriteAllText(RecipeBookPath, updatedRecipes);
        }

        private static string SerializeRecipe(Recipe recipe)
        {
            return JsonSerializer.Serialize(recipe, new JsonSerializerOptions { WriteIndented = true });
        }

        private static string SerializeRecipes(List<Recipe> recipes)
        {
            return JsonSerializer.Serialize(recipes, new JsonSerializerOptions { WriteIndented = true });
        }

        private static Recipe? DeserializeRecipe(string jsonString)
        {
            return JsonSerializer.Deserialize<Recipe>(jsonString);
        }

        private static List<Recipe> DeserializeRecipes(string jsonString)
        {
            List<Recipe> recipes;
            try
            {
                recipes = JsonSerializer.Deserialize<List<Recipe>>(jsonString) ?? new();
            }
            catch (Exception)
            {
                recipes = new();
            }
            return recipes;
        }

    }
}
