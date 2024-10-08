﻿using Mango.Web.DAL.Enums;

namespace Mango.Web.DAL.Models.Dtos.Request
{
	public class RequestDto
	{
		public ApiType ApiType { get; set; } = ApiType.GET;
		public string Url { get; set; }
		public object Data { get; set; }
		public string AccessToken { get; set; }
	}
}
