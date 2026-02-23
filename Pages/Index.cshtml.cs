using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesArenaCalculator.Pages;

public class IndexModel : PageModel
{
    public static readonly Dictionary<int, (double Modifier, string Label)> ArenaBrackets = new()
    {
        { 2, (0.76, "2v2") },
        { 3, (0.88, "3v3") },
        { 5, (1.00, "5v5") }
    };

    [BindProperty]
    public int Bracket { get; set; } = 5;

    [BindProperty]
    public int Rating { get; set; } = 1500;

    public double? Result { get; set; }

    public void OnGet() { }

    public void OnPost()
    {
        if (!ArenaBrackets.ContainsKey(Bracket))
        {
            ModelState.AddModelError(nameof(Bracket), "Please select a valid bracket.");
            return;
        }

        if (Rating < 0 || Rating > 9999)
        {
            ModelState.AddModelError(nameof(Rating), "Rating must be between 0 and 9999.");
            return;
        }

        double modifier = ArenaBrackets[Bracket].Modifier;
        Result = Math.Floor(
            (((1651.94 - 475) / (1 + 2500000 * Math.Pow(2.71828, -0.009 * Rating)) + 475) * 1.5) * modifier
        );
    }
}
