"use strict";

angular.module("psapp")
    .service("DashboardService",
    [
        function() {

            return {

                get: function() {
                    this.data = [
                        {
                            Id: 1,
                            RequestId: "001",
                            ExporterNo: "BD0001",
                            ExporterName: "Next Generation Ltd.",
                            Date: "25 Oct. 2015",
                            Quantity: "3",
                            Amount: "3000",
                            State: { Id: 1, Name: "Requested" }
                        },
                        {
                            Id: 2,
                            RequestId: "002",
                            ExporterNo: "BD0001",
                            ExporterName: "Next Generation Ltd.",
                            Date: "25 Oct. 2015",
                            Quantity: "5",
                            Amount: "2800",
                            State: { Id: 1, Name: "Requested" }
                        },
                        {
                            Id: 3,
                            RequestId: "003",
                            ExporterNo: "BD0002",
                            ExporterName: "Amir Ltd.",
                            Date: "25 Oct. 2015",
                            Quantity: "2",
                            Amount: "3000",
                            StateId: 1,
                            State: { Id: 1, Name: "Requested" }
                        },
                        {
                            Id: 4,
                            RequestId: "004",
                            ExporterNo: "BD0003",
                            ExporterName: "Index Text. Ltd.",
                            Date: "25 Oct. 2015",
                            Quantity: "9",
                            Amount: "3500",
                            State: { Id: 2, Name: "Approved" }
                        },
                        {
                            Id: 5,
                            RequestId: "005",
                            ExporterNo: "BD0003",
                            ExporterName: "Index Text. Ltd.",
                            Date: "25 Oct. 2015",
                            Quantity: "10",
                            Amount: "4000",
                            State: { Id: 1, Name: "Requested" }
                        },
                        {
                            Id: 6,
                            RequestId: "006",
                            ExporterNo: "BD0003",
                            ExporterName: "Index Text. Ltd.",
                            Date: "25 Oct. 2015",
                            Quantity: "1",
                            Amount: "6000",
                            State: { Id: 1, Name: "Requested" }
                        }
                    ];

                    return this.data;
                },

                exporterData : function() {
                    this.data = [
                         {
                             Id: 1,
                             RequestId: "001",
                             ExporterNo: "BD0001",
                             ExporterName: "Next Generation Ltd.",
                             Date: "25 Oct. 2015",
                             Quantity: "3",
                             Amount: "3000",
                             State: { Id: 1, Name: "Requested" }
                         },
                        {
                            Id: 2,
                            RequestId: "002",
                            ExporterNo: "BD0001",
                            ExporterName: "Next Generation Ltd.",
                            Date: "25 Oct. 2015",
                            Quantity: "5",
                            Amount: "2800",
                            State: { Id: 1, Name: "Requested" }
                        },
                        {
                            Id: 3,
                            RequestId: "003",
                            ExporterNo: "BD0001",
                            ExporterName: "Next Generation Ltd.",
                            Date: "25 Oct. 2015",
                            Quantity: "2",
                            Amount: "3000",
                            StateId: 1,
                            State: { Id: 1, Name: "Requested" }
                        },
                        {
                            Id: 4,
                            RequestId: "004",
                            ExporterNo: "BD0001",
                            ExporterName: "Next Generation Ltd.",
                            Date: "25 Oct. 2015",
                            Quantity: "9",
                            Amount: "3500",
                            State: { Id: 2, Name: "Approved" }
                        },
                        {
                            Id: 5,
                            RequestId: "005",
                            ExporterNo: "BD0001",
                            ExporterName: "Next Generation Ltd.",
                            Date: "25 Oct. 2015",
                            Quantity: "10",
                            Amount: "4000",
                            State: { Id: 1, Name: "Requested" }
                        },
                        {
                            Id: 6,
                            RequestId: "006",
                            ExporterNo: "BD0001",
                            ExporterName: "Next Generation Ltd.",
                            Date: "25 Oct. 2015",
                            Quantity: "1",
                            Amount: "6000",
                            State: { Id: 1, Name: "Requested" }
                        }
                    ];

                    return this.data;
                }
            };
        }
    ]);