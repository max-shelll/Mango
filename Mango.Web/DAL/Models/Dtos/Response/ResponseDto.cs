﻿namespace Mango.Web.DAL.Models.Dtos.Response
{
	public class ResponseDto
	{
		public object? Result { get; set; }
		public bool IsSuccess { get; set; } = true;
		public string Message { get; set; } = "";
	}
}
