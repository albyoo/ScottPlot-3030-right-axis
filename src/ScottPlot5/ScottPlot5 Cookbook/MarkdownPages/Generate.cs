﻿namespace ScottPlotCookbook.MarkdownPages;

internal class Generate
{
    [Test]
    public void Generate_Markdown_Pages()
    {
        // this test assumes jpegs have already been generated by other tests
        // TODO: make this test run last as part of the breakdown
        List<WebRecipe> sources = SourceReading.GetWebRecipes();
        GenerateHomePage(sources);
        GenerateCategoryPages(sources);
        GenerateRecipePages(sources);
        Console.WriteLine(Cookbook.OutputFolder);
    }

    private void GenerateHomePage(List<WebRecipe> sources)
    {
        TestContext.WriteLine("Generating cookbook home page");
        FrontPage frontPage = new(sources);
        frontPage.Generate();
    }

    private void GenerateCategoryPages(List<WebRecipe> sources)
    {
        List<CategoryInfo> knownPages = Query.GetCategories();

        TestContext.WriteLine("Generating cookbook category pages");
        foreach (string category in sources.Select(x => x.Category).Distinct())
        {
            foreach (WebRecipe source in sources.Where(x => x.Category == category))
            {
                CategoryPage cp = new(sources, category);
                cp.Generate();
            }
        }
    }

    private void GenerateRecipePages(List<WebRecipe> sources)
    {
        TestContext.WriteLine("Generating cookbook recipe pages");
        foreach (RecipeInfo recipe in Query.GetRecipes())
        {
            RecipePage recipePage = new(recipe);
            recipePage.Generate();
        }
    }
}
