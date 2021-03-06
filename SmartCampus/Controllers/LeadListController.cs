﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SmartCampus.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCampus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadListController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public LeadListController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {

            string query = @"
                             select ProfileId,ProfileName,OriginType,EmailAddress,ContactNumber,
                             Nationality,LeadScoring,LeadStatus,ProfileType,convert(varchar(10),RegistrationDate,120) as RegistrationDate from LeadListProfile";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("IdentityConnection");

            SqlDataReader myReader;


            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            // using (var cmd = new SqlCommand(query, con))
            //using (var da = new SqlDataAdapter(cmd))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }

            }
            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult Post(LeadListProfiles llp)
        {
            string query = @"INSERT INTO LeadListProfile (ProfileName,OriginType,EmailAddress,ContactNumber,LeadScoring,LeadStatus,ProfileType,RegistrationDate,Nationality) VALUES ('" +
               llp.profileName + "','" +
                llp.originType + "','" +
                llp.emailAddress + "','" +
                llp.contactNumber + "','" +
                llp.leadScoring + "','" +
                llp.leadStatus + "','" +
                llp.profileType + "','" +
                llp.registrationDate + "','" +
                llp.nationality + "')";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("IdentityConnection");

            SqlDataReader myReader;


            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            // using (var cmd = new SqlCommand(query, con))
            //using (var da = new SqlDataAdapter(cmd))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }


            }
            return new JsonResult("Added Sucessfully");

        }

        [HttpPut]
        public JsonResult Put(LeadListProfiles llp)
        {

            string query = @"UPDATE LeadListProfile SET 
                                 ProfileName =  '" + llp.profileName +
                              "',OriginType =  '" + llp.originType +
                              "',EmailAddress =  '" + llp.emailAddress +
                              "',ContactNumber =  '" + llp.contactNumber +
                              "',ProfileType =  '" + llp.profileType +
                              "',LeadScoring =  '" + llp.leadScoring +
                              "',LeadStatus =  '" + llp.leadStatus +
                              "',RegistrationDate =  '" + llp.registrationDate +
                              "',Nationality =  '" + llp.nationality +

                    "'  WHERE ProfileId='" + llp.profileId + "' ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("IdentityConnection");

            SqlDataReader myReader;


            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            // using (var cmd = new SqlCommand(query, con))
            //using (var da = new SqlDataAdapter(cmd))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }

            }
            return new JsonResult("Update Sucessfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = "DELETE FROM LeadListProfile WHERE ProfileId='" + id + "'";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("IdentityConnection");

            SqlDataReader myReader;


            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            // using (var cmd = new SqlCommand(query, con))
            //using (var da = new SqlDataAdapter(cmd))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }


            }
            return new JsonResult("Delete Sucessfully");

        }




    }
}
