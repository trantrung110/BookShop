﻿using System;
using System.Collections.Generic;

namespace CT288Book.Data;

public partial class ChiTietHd
{
    public int MaCt { get; set; }

    public int MaHd { get; set; }

    public int MaHh { get; set; }

    public double DonGia { get; set; }

    public int SoLuong { get; set; }

    public double GiamGia { get; set; }

    public virtual HoaDon MaHdNavigation { get; set; } = null!;

    public virtual Book MaHhNavigation { get; set; } = null!;
}
