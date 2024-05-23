using System;
using System.Collections.Generic;

namespace lab6.Entities;

public partial class History
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }

    public string Operation { get; set; } = null!;

    public string InputText { get; set; } = null!;

    public long Key { get; set; }

    public string OutputText { get; set; } = null!;
}
