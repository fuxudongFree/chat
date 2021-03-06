﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication2.core;
using WindowsFormsApplication2.data;

namespace WindowsFormsApplication2.dataEntry
{
 public   class user
    {
        public  List<device> devices = new List<device>();
        userList list;
        uint id;
        /// <summary>
        /// 接受的消息集合
        /// 发送的消息集合怎么办
        /// </summary>
       
        public  user(ref userList list,uint id)
        {
            this.list = list;
            this.id = id;
        }


        public void checkNotSendMessage()
        {
            if (devices.Count != 0)
            {
                while (list.getUser(id).notReadMess.Count != 0)
                {
                    communication temp = list.getUser(id).notReadMess.Dequeue();
                    sendCommToDev(temp);
                    list.getUser(id).readMess.Add(temp);
                }
            }
        }

        public  void pushComm(communication comm)
        {
            if (devices.Count == 0)
            {
                list.getUser(comm.to).notReadMess.Enqueue(comm);
            }
            else
            {
                userdata to = list.getUser(comm.to);
                while (to.notReadMess.Count!=0)
                {
                    communication temp = to.notReadMess.Dequeue();
                    sendCommToDev(temp);
                    to.readMess.Add(temp);
                }
                sendCommToDev(comm);
                to.readMess.Add(comm);
            }          
        }

        private void sendCommToDev(communication com)
        {
            foreach (device de in devices)
            {
                de.sendMessage(com);
            }
        }

    }
}
