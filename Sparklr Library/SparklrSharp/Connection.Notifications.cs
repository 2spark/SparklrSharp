﻿using SparklrSharp.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp
{
    public partial class Connection
    {
        internal async Task<Sparklr.Notification[]> GetNotificationsAsync()
        {
            SparklrResponse<JSONRepresentations.Get.Notification[]> result = await webClient.GetJSONResponseAsync<JSONRepresentations.Get.Notification[]>("notifications");

            if (result.Code == System.Net.HttpStatusCode.OK)
            {
                Sparklr.Notification[] notifications = new Sparklr.Notification[result.Response.Length];
                
                for(int i = 0; i < result.Response.Length; i++)
                {
                    notifications[i] = new Sparklr.Notification(
                            result.Response[i].id,
                            await Sparklr.User.InstanciateUserAsync(result.Response[i].from, this),
                            await Sparklr.User.InstanciateUserAsync(result.Response[i].to, this),
                            (Sparklr.NotificationType)result.Response[i].type,
                            result.Response[i].time,
                            result.Response[i].body,
                            result.Response[i].action
                        );
                }

                return notifications;
            }
            else
            {
                throw new Exceptions.NoDataFoundException();
            }
        }
    }
}
