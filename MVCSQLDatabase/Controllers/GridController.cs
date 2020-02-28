﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MVCSQLDatabase.Models;

namespace MVCSQLDatabase.Controllers
{
    public class GridController : Controller
    {
        private DBModels db = new DBModels();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Proposals_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Proposal> proposals = db.Proposals;
            DataSourceResult result = proposals.ToDataSourceResult(request, proposal => new {
                Proposal_Uid = proposal.Proposal_Uid,
                Prime_Contract = proposal.Prime_Contract,
                Proposal_Title = proposal.Proposal_Title,
                Client_Name = proposal.Client_Name,
                Client_Code = proposal.Client_Code,
                Total_Proposal_Amount = proposal.Total_Proposal_Amount,
                Manager_Name = proposal.Manager_Name,
                Start_Date = proposal.Start_Date,
                End_Date = proposal.End_Date,
                Proposal_Number = proposal.Proposal_Number,
                Contract_Type = proposal.Contract_Type
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Proposals_Create([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Proposal> proposals)
        {
            var entities = new List<Proposal>();
            if (proposals != null && ModelState.IsValid)
            {
                foreach(var proposal in proposals)
                {
                    var entity = new Proposal
                    {
                            Prime_Contract = proposal.Prime_Contract,
                            Proposal_Title = proposal.Proposal_Title,
                            Client_Name = proposal.Client_Name,
                            Client_Code = proposal.Client_Code,
                            Total_Proposal_Amount = proposal.Total_Proposal_Amount,
                            Manager_Name = proposal.Manager_Name,
                            Start_Date = proposal.Start_Date,
                            End_Date = proposal.End_Date,
                            Proposal_Number = proposal.Proposal_Number,
                            Contract_Type = proposal.Contract_Type
                    };

                    db.Proposals.Add(entity);
                    entities.Add(entity);
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Proposals_Update([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Proposal> proposals)
        {
            var entities = new List<Proposal>();
            if (proposals != null && ModelState.IsValid)
            {
                foreach(var proposal in proposals)
                {
                    var entity = new Proposal
                    {
                        Proposal_Uid = proposal.Proposal_Uid,
                        Prime_Contract = proposal.Prime_Contract,
                        Proposal_Title = proposal.Proposal_Title,
                        Client_Name = proposal.Client_Name,
                        Client_Code = proposal.Client_Code,
                        Total_Proposal_Amount = proposal.Total_Proposal_Amount,
                        Manager_Name = proposal.Manager_Name,
                        Start_Date = proposal.Start_Date,
                        End_Date = proposal.End_Date,
                        Proposal_Number = proposal.Proposal_Number,
                        Contract_Type = proposal.Contract_Type
                    };

                    entities.Add(entity);
                    db.Proposals.Attach(entity);
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;                        
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        } 

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Proposals_Destroy([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Proposal> proposals)
        {
            var entities = new List<Proposal>();
            if (ModelState.IsValid)
            {
                foreach(var proposal in proposals)
                {
                    var entity = new Proposal
                    {
                        Proposal_Uid = proposal.Proposal_Uid,
                        Prime_Contract = proposal.Prime_Contract,
                        Proposal_Title = proposal.Proposal_Title,
                        Client_Name = proposal.Client_Name,
                        Client_Code = proposal.Client_Code,
                        Total_Proposal_Amount = proposal.Total_Proposal_Amount,
                        Manager_Name = proposal.Manager_Name,
                        Start_Date = proposal.Start_Date,
                        End_Date = proposal.End_Date,
                        Proposal_Number = proposal.Proposal_Number,
                        Contract_Type = proposal.Contract_Type
                    };

                    entities.Add(entity);
                    db.Proposals.Attach(entity);
                    db.Proposals.Remove(entity);
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
