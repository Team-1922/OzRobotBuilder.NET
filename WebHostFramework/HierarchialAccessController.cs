using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Team1922.WebFramework
{
    [Route("api/Robot")]
    public class HierarchialAccessController
    {
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
                return new ObjectResult(RobotRepository.Instance[key.Replace('/','.')]);
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
        public IActionResult PatchById(string key, [FromBody] string value)
        {
            try
            {
                RobotRepository.Instance[key] = value;
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
            return new StatusCodeResult(503);
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
    }
}
