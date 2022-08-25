﻿namespace WheelsAndGoods.Application.Models.Paginations;

public abstract class PaginationRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; }
}
