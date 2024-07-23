using System;
using System.Collections.Generic;

namespace InternshipDotNetCore.DatabaseFirstAuto.Models;

public partial class TblPageStatistic
{
    public int PageStatistics { get; set; }

    public int SessionDuration { get; set; }

    public int PageViews { get; set; }

    public int TotalVisits { get; set; }

    public string CreatedDate { get; set; } = null!;
}
