using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Team1922.WebFramework
{/*
    [Route("api/Robot")]
    public class HierarchialAccessController
    {
        public static string ConvertPath(string key)
        {
            return key.Replace('/', '.');
        }

        public static string JsonObjectToString(object jsonObject)
        {
            return "";
        }

        [HttpGet]
        public IActionResult Get()
        {
            return GetById("");
        }

        // GET api/robot/AnalogInputSampleRate
        [HttpGet("{*key}", Name = "GetValue")]
        public IActionResult GetById(string key)
        {
            try
            {
                return new ObjectResult(RobotRepository.Instance[ConvertPath(key)]);
            }
            catch(ArgumentException)
            {
                return new NotFoundResult();
            }
            catch(Exception e)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPatch("{*key}", Name = "PatchValue")]
        public IActionResult PatchById(string key, [FromBody] object value)
        {
            try
            {
                try
                {
                    // TODO: this might be a slow way to do it
                    var result = RobotRepository.Instance[ConvertPath(key)];
                }
                catch(ArgumentException e)
                {
                    //not found
                    return new NotFoundResult();
                }
                //only do this operation if it exists already
                RobotRepository.Instance[key] = JsonConvert.SerializeObject(value, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                return new OkResult();
            }
            catch (ArgumentException)
            {
                return new NotFoundResult();
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("{*key}", Name = "PostValue")]
        public IActionResult PostById(string key, [FromBody] string value)
        {
            try
            {
                // TODO: this might be a slow way to do it
                var result = RobotRepository.Instance[ConvertPath(key)];
                return new StatusCodeResult(409);//already exists, therefore don't do it
            }
            catch (ArgumentException)
            {
                try
                {
                    //only do this operation if it does not exists already
                    RobotRepository.Instance[ConvertPath(key)] = value;
                    return new CreatedResult(key, value);//TODO: i think this is wrong
                }
                catch (ArgumentException)
                {
                    return new NotFoundResult();
                }
                catch(Exception)
                {
                    return new StatusCodeResult(500);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPut("{*key}", Name = "PutValue")]
        public IActionResult PutById(string key, [FromBody] string value)
        {
            return new StatusCodeResult(503);
        }

        [HttpDelete("{*key}", Name = "DeleteValue")]
        public IActionResult DeleteById(string key)
        {
            return new StatusCodeResult(503);
        }
    }*/
}
