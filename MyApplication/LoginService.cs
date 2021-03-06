﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Service
{
    using Http;
    using com.force.json;
    using Utils;

    class LoginService
    {
        private static LoginService loginService = new LoginService();

        private bool isLogin = false;
        private int userId = -1;
        private String username = null;
        private String password = null;

        private HttpClient httpClient = new HttpClient();

        public static LoginService getInstance()
        {
            return loginService;
        }

        private LoginService()
        {
        }

        public bool IsLogin
        {
            get
            {
                return isLogin;
            }
        }

        public int UserId
        {
            get
            {
                return userId;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
        }


        public bool login(String username, String password)
        {
            String res = httpClient.Url(Constant.SERVER_LOGIN_URL)
                .Param("username", username)
                .Param("password", password)
                .Post();

            JSONObject resObj = new JSONObject(res);
            bool success = bool.Parse(resObj.GetString("success"));
            if(success)
            {
                this.isLogin = true;
                this.userId = resObj.GetInt("data");
                this.username = username;
                this.password = password;
            }
            return success;
        }

        public void logout()
        {
            this.isLogin = false;
            this.username = null;
            this.password = null;
        }
        


    }
}
