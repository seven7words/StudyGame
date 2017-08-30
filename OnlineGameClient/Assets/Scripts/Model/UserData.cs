﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


  public  class UserData
    {
        public string Username { get; private set; }
        public int TotalCount { get; private set; }
        public int WinCount { get; private set; }
        public int Id { get; set; }

        public UserData(string userData)
        {
            string[] strs = userData.Split(',');
            this.Id = int.Parse(strs[0]);
            this.Username = strs[1];
        this.TotalCount = int.Parse(strs[2]);
            this.WinCount = int.Parse(strs[3]);
    }
        public UserData(string username, int totalCount, int winCount)
        {
            this.Username = username;
            this.TotalCount = totalCount;
            this.WinCount = winCount;
        }
        public UserData(int id,string username, int totalCount, int winCount)
        {
            this.Id = id;
            this.Username = username;
            this.TotalCount = totalCount;
            this.WinCount = winCount;
        }
}

