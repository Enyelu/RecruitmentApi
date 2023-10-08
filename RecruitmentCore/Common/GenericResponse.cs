﻿using Newtonsoft.Json;

namespace RecruitmentCore.Common
{
    public class GenericResponse<T> where T : class
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Succeeded { get; set; }

        public GenericResponse(int statusCode, bool success, string msg, T data)
        {
            Data = data;
            Succeeded = success;
            StatusCode = statusCode;
            Message = msg;
        }
        public GenericResponse()
        {
        }

        public static GenericResponse<T> Fail(string errorMessage, int statusCode = 400)
        {
            return new GenericResponse<T> { Succeeded = false, Message = errorMessage, StatusCode = statusCode };
        }

        public static GenericResponse<T> NotFound(string errorMessage, int statusCode = 404)
        {
            return new GenericResponse<T> { Succeeded = false, Message = errorMessage, StatusCode = statusCode };
        }

        public static GenericResponse<T> Success(T data, string successMessage, int statusCode = 200)
        {
            return new GenericResponse<T> { Data = data, Succeeded = true, Message = successMessage, StatusCode = statusCode };
        }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
