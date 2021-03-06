﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpertSystemShell.Parsers.Grammars;
using ExpertSystemShell.Parsers.Grammars.ProductionModel;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using ExpertSystemShell.Builders;
using ExpertSystemShell.Expressions;
using ExpertSystemShell.Parsers;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.Solvers.ProductionModel;
using ExpertSystemShell.Solvers;
using ExpertSystemShell.KnowledgeBases.StorageServices;
using ExpertSystemShell;
using System.IO;

namespace Tests
{
    [TestClass]
    public class ProductionSolversTest
    {
        IExpertSystem expert;

        [TestMethod]
        public void TestDirectSolver()
        {
            IKnowledgeBase knBase = new ProdModelSimpleKnBase(new PrMInMemoryStService());
            ILogicalSolver solver = new DirectProductionSolver(knBase);
            LogicalExpressionHelper eh = new LogicalExpressionHelper();
            IParser parser = new PrModelParser(eh);
            expert = new ExpertSystemBase(knBase, solver, parser);
            string rules = Properties.Resources.knowledgeBase;
            expert.AddRules(rules);
            string query = "если 'валютный курс доллара - падает' то 'уровень цен на бирже=?'";
            ILogicalResult result = expert.GetResult(query);
            Assert.IsTrue(result is ResultingFactSet);
            Assert.IsTrue(result.ToString() == "'уровень цен на бирже - падает'");
        }

        [TestMethod]
        public void TestReverseSolver()
        {
            IKnowledgeBase knBase = new ProdModelSimpleKnBase(new PrMInMemoryStService());
            ILogicalSolver solver = new ReverseProductionSolver(knBase);
            LogicalExpressionHelper eh = new LogicalExpressionHelper();
            IParser parser = new PrModelParser(eh);
            expert = new ExpertSystemBase(knBase, solver, parser);
            string rules = Properties.Resources.knowledgeBase;
            expert.AddRules(rules);
            string query = "если 'валютный курс доллара - падает' то 'уровень цен на бирже=?'";
            ILogicalResult result = expert.GetResult(query);
            Assert.IsTrue(result is ResultingFactSet);
            Assert.IsTrue(result.ToString() == "'уровень цен на бирже - падает'");
        }

        [TestMethod]
        public void TestDirectReteNetworkSolver()
        {
            IKnowledgeBase knBase = new ProductionModelReteNetwork(new PrMInMemoryStService());
            ILogicalSolver solver = new DirectProductionSolver(knBase);
            LogicalExpressionHelper eh = new LogicalExpressionHelper();
            IParser parser = new PrModelParser(eh);
            expert = new ExpertSystemBase(knBase, solver, parser);
            string rules = Properties.Resources.knowledgeBase;
            expert.AddRules(rules);
            string query = "если 'валютный курс доллара - падает' то 'уровень цен на бирже=?'";
            ILogicalResult result = expert.GetResult(query);
            Assert.IsTrue(result is ResultingFactSet);
            Assert.IsTrue(result.ToString() == "'уровень цен на бирже - падает'");
        }
    }
}
