﻿using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using vHackApi.Api;
using vHackApi.Interfaces;

namespace vHackApi
{
    public class Update
    {
        private IConfig config;

        public Update(IConfig conf)
        {
            config = conf;
        }

        public async Task<JObject> getTasks()
        {
            var temp = await vhUtils.JSONRequest("user::::pass::::uhash",
                config.username + "::::" + config.password + "::::" + "userHash_not_needed",
                "vh_tasks.php");
            //return (JArray)temp["data"];
            return temp;
        }

        public async Task<JArray> SpywareInfo()
        {
            var temp = await vhUtils.JSONRequest("user::::pass::::uhash:::::",
                config.username + "::::" + config.password + "::::" + "UserHash_not_needed" + ":::::",
                "vh_spywareInfo.php");
            return (JArray) temp["data"];
        }

        public async Task<JObject> removeSpyware()
        {
            return await vhUtils.JSONRequest("user::::pass::::uhash:::::",
                config.username + "::::" + config.password + "::::" + "UserHash_not_needed" + ":::::",
                "vh_removeSpyware.php");
        }

        public async Task<int> getTaskAmount()
        {
            var temp = await getTasks();
            return temp.Count;
        }

        public async Task<string[]> getTaskIDs()
        {
            var temp = await getTasks();
            var tasks = (JArray) temp["data"];
            return tasks.Select(it => (string) it["taskid"]).ToArray();
        }

        public async Task<JObject> startTask(Tasks type)
        {
            var temp = await vhUtils.JSONRequest("user::::pass::::uhash::::utype",
                config.username + "::::" + config.password + "::::" + "userHash_not_needed" + "::::" + type.ToString(),
                "vh_addUpdate.php");
            //var res = (int)temp["result"]; // 0 ok, 3 full
            return temp;
        }
        public async Task<bool> finishTask(string taskID)
        {
            throw new NotImplementedException(); // not working

            var res = await vhUtils.JSONRequest("user::::pass::::uhash::::taskid",
                config.username + "::::" + config.password + "::::" + "userHash_not_needed" + "::::" + taskID,
                "vh_finishTask.php");
            if ((int) res == 4)
                return true;

            return false;
        }

        public async Task<JObject> finishAll()
        {
            return await vhUtils.JSONRequest("user::::pass::::uhash",
                config.username + "::::" + config.password + "::::" + "userHash_not_needed",
                "vh_finishAll.php");
        }

        public async Task<JObject> useBooster()
        {
            return await vhUtils.JSONRequest("user::::pass::::uhash::::boost",
                config.username + "::::" + config.password + "::::" + "userHash_not_needed" + "::::" + "1",
                "vh_tasks.php");
        }

        //public async void doTasks(TimeSpan wait)
        //{
        //    foreach (var update in config.updates)
        //    {
        //        config.logger.Log("updating {0} level +1", update);
        //        Thread.Sleep(wait);
        //        var res = await this.startTask(config.updates.Last());
        //        if (res == 0)
        //            config.logger.Log("update failed");
        //    }
        //}

        #region BOTNET

        public async Task<JArray> botnetInfo()
        {
            /*
             * {"c2":"0","count":"3","energy":"187","pieces":"24","money":"440820118","nref":"0","data":
             * [
             * {"running":"0","wto":"0","left":"0","hostname":"WbBotnetPc001","fw":"3","av":"4","smash":"15","mwk":"2","strength":"72"},
             * {"running":"0","wto":"0","left":"0","hostname":"WbBotnetPc_001","fw":"4","av":"2","smash":"14","mwk":"1","strength":"63"},
             * {"running":"0","wto":"0","left":"0","hostname":"WbBotnetPc_003","fw":"3","av":"1","smash":"14","mwk":"1","strength":"57"}
             * ],
             * "strength":192}
             */
            var temp = await vhUtils.JSONRequest("user::::pass::::uhash",
                config.username + "::::" + config.password + "::::" + "userHash_not_needed",
                "vh_botnetInfo.php");
            return (JArray)temp["data"];
        }



        public enum OfWhat
        {
            mwk,
            fw,
            av,
            smash
        }

        public async Task<JObject> upgradePC(string userHash, string hostname, OfWhat ofWhat)
        {
            /*
             
             */

            return await vhUtils.JSONRequest("user::::pass::::uhash::::hostname::::ofwhat",
                $"{config.username}::::{config.password}::::{userHash}::::{hostname}::::{ofWhat.ToString()}",
                "vh_upgradePC.php");
        }

        /// <summary>
        /// Creates new botnet pc
        /// </summary>
        /// <param name="userHash"></param>
        /// <param name="hostname"></param>
        /// <returns></returns>
        public async Task<JObject> buildPC(string userHash, string hostname)
        {
            /*
             {"result":"5","count":"2","energy":"195","pieces":"40","money":"1350047","nref":"0","data":[{"running":"0","wto":"0","left":"0","hostname":"WbBotnetPc001","fw":"1","av":"1","smash":"1","mwk":"1","strength":"12"},{"running":"0","wto":"0","left":"0","hostname":"WbBotnetPc_001","fw":"1","av":"1","smash":"1","mwk":"1","strength":"12"}],"strength":24}{"result":"5","count":"2","energy":"195","pieces":"40","money":"1350047","nref":"0","data":[{"running":"0","wto":"0","left":"0","hostname":"WbBotnetPc001","fw":"1","av":"1","smash":"1","mwk":"1","strength":"12"},{"running":"0","wto":"0","left":"0","hostname":"WbBotnetPc_001","fw":"1","av":"1","smash":"1","mwk":"1","strength":"12"}],"strength":24}
             */
            return await vhUtils.JSONRequest("user::::pass::::uhash::::hostname",
                $"{config.username}::::{config.password}::::{userHash}::::{hostname}",
                "vh_buildPC.php");
        }

        /// <summary>
        /// Get status for botnet career
        /// </summary>
        /// <param name="userHash"></param>
        /// <returns></returns>
        public async Task<JObject> getCareerStatus(string userHash)
        {
            // {"count":"3","strength":36,"energy":"195","nextlevel":"1"}
            return await vhUtils.JSONRequest("user::::pass::::uhash",
                $"{config.username}::::{config.password}::::{userHash}",
                "vh_getCareerStatus.php");
        }

        /// <summary>
        /// Starts an attack (chapter divided by levels)
        /// </summary>
        /// <param name="userHash"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<JObject> startLevel(string userHash, int level)
        {
            return await vhUtils.JSONRequest("user::::pass::::uhash::::lvl",
                $"{config.username}::::{config.password}::::{userHash}::::{level}",
                "vh_startLevel.php");
        }

        //public async Task<JObject> upgradeBotnet(string ID)
        //{
        //    return await vhUtils.JSONRequest("user::::pass::::uhash::::bID",
        //                        config.username + "::::" + config.password + "::::" + "userHash_not_needed" + "::::" + ID,
        //                        "vh_upgradeBotnet.php");
        //}



        #endregion

        #region PACKAGES
        //public async Task<JObject> openPackage(string userHash)
        //{
        //    return await vhUtils.JSONRequest("user::::pass::::uhash",
        //        config.username + "::::" + config.password + "::::" + "userHash_not_needed", "vh_openFreeBonus.php", 5);
        //}

        //public async Task<JObject> openAllPackages(string userHash)
        //{
        //    return await vhUtils.JSONRequest("user::::pass::::uhash",
        //        config.username + "::::" + config.password + "::::" + "userHash_not_needed", "vh_openAllBonus.php");
        //}

        /// <summary>
        /// Open one package
        /// </summary>
        /// <param name="userHash"></param>
        /// <returns></returns>
        public async Task<JObject> openFreeBonus(string userHash)
        {
            return await vhUtils.JSONRequest("user::::pass::::uhash",
                $"{config.username}::::{config.password}::::{userHash}",
                "vh_openFreeBonus.php");
        }
        /// <summary>
        /// Open all packages
        /// </summary>
        /// <param name="userHash"></param>
        /// <returns></returns>
        public async Task<JObject> openAllBonus(string userHash)
        {
            return await vhUtils.JSONRequest("user::::pass::::uhash",
                $"{config.username}::::{config.password}::::{userHash}",
                "vh_openAllBonus.php");
        }
        /// <summary>
        /// Open one gold package
        /// </summary>
        /// <param name="userHash"></param>
        /// <returns></returns>
        public async Task<JObject> openGoldBox(string userHash)
        {
            return await vhUtils.JSONRequest("user::::pass::::uhash",
                $"{config.username}::::{config.password}::::{userHash}",
                "vh_openGoldBox.php");
        }
        /// <summary>
        /// Open all gold packages
        /// </summary>
        /// <param name="userHash"></param>
        /// <returns></returns>
        public async Task<JObject> openAllGold(string userHash)
        {
            return await vhUtils.JSONRequest("user::::pass::::uhash",
                $"{config.username}::::{config.password}::::{userHash}",
                "vh_openAllGold.php");
        }


        #endregion

        public async Task<JObject> getSysLog(string userHash = "")
        {
            return string.IsNullOrEmpty(userHash)
                ? await vhUtils.JSONRequest("user::::pass::::uhash",
                    config.username + "::::" + config.password + "::::" + "userHash_not_needed", "vh_getSysLog.php")
                : await vhUtils.JSONRequest("user::::pass::::uhash",
                    config.username + "::::" + config.password + "::::" + userHash, "vh_getSysLog.php");
        }

        public async Task<JObject> getRanking(string userHash = "")
        {
            if (string.IsNullOrEmpty(userHash))
                userHash = "userHash_not_needed";

            return await vhUtils.JSONRequest("user::::pass::::uhash",
                $"{config.username}::::{config.password}::::{userHash}", "vh_ranking.php");
        }

        public async Task<JObject> getMails(string userHash = "")
        {
            if (string.IsNullOrEmpty(userHash))
                userHash = "userHash_not_needed";

            return await vhUtils.JSONRequest("user::::pass::::uhash::::action",
                $"{config.username}::::{config.password}::::{userHash}::::list", "vh_mails.php", 3);
        }

        public async Task<string> readMail(int id, string userHash = "")
        {
            if (string.IsNullOrEmpty(userHash))
                userHash = "userHash_not_needed";

            return await vhUtils.StringRequest("user::::pass::::uhash::::action::::mID",
                $"{config.username}::::{config.password}::::{userHash}::::getmail::::{id}", "vh_mails.php");
        }

    }
}