﻿namespace IbFlexReader.Tests.Xml
{
    using FluentAssertions;
    using IbFlexReader.Xml;
    using NUnit.Framework;
    using FlexQueryResponse = IbFlexReader.Xml.Contracts.QueryResponse.FlexQueryResponse;

    public class ChashTransactionTest
    {
        private IStreamBuilder<string> streamBuilder;

        [SetUp]
        public void InitTests()
        {
            streamBuilder = new StringStream();
        }

        [Test]
        public void BrokerInterestReceived()
        {
            var str = StringFactory.XmlStart + @"<CashTransactions>
            <CashTransaction type='Broker Interest Received' />
            </CashTransactions>" + StringFactory.XmlEnd;
            var obj = Deserializer.Deserialize<FlexQueryResponse, Contracts.FlexQueryResponse>(streamBuilder.GenerateStream(str), out var msg);
            var cashTransactions = obj.FlexStatements.FlexStatement.CashTransactions.CashTransaction;
            cashTransactions.Count.Should().Be(1);
            cashTransactions[0].Type.Should().Be(Contracts.Enums.CashTransactionType.BrokerInterestReceived);
        }
    }
}
