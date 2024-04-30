﻿namespace Mango.Services.ProductAPI.DAL.Models.Dtos
{
	public class ResponseDto
	{
		public object? Result { get; set; }
		public bool IsSuccess { get; set; } = true;
		public string Message { get; set; } = "";
	}
}
