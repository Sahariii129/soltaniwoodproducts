var reportorder2 = {
    "ReportVersion": "2018.3.5.0",
    "ReportGuid": "2bea9371e68a4955b2348b8660f2c208",
    "ReportName": "Report",
    "ReportAlias": "Report",
    "ReportCreated": "/Date(1519224897000+0330)/",
    "ReportChanged": "/Date(1542545770000+0330)/",
    "EngineVersion": "EngineV2",
    "ReportUnit": "Inches",
    "Script": "using System;\r\nusing System.Drawing;\r\nusing System.Windows.Forms;\r\nusing System.Data;\r\nusing Stimulsoft.Controls;\r\nusing Stimulsoft.Base.Drawing;\r\nusing Stimulsoft.Report;\r\nusing Stimulsoft.Report.Dialogs;\r\nusing Stimulsoft.Report.Components;\r\n\r\nnamespace Reports\r\n{\r\n    public class Report : Stimulsoft.Report.StiReport\r\n    {\r\n        public Report()        {\r\n            this.InitializeComponent();\r\n        }\r\n\r\n        #region StiReport Designer generated code - do not modify\r\n\t\t#endregion StiReport Designer generated code - do not modify\r\n    }\r\n}\r\n",
    "ReferencedAssemblies": {
        "0": "System.Dll",
        "1": "System.Drawing.Dll",
        "2": "System.Windows.Forms.Dll",
        "3": "System.Data.Dll",
        "4": "System.Xml.Dll",
        "5": "Stimulsoft.Controls.Dll",
        "6": "Stimulsoft.Base.Dll",
        "7": "Stimulsoft.Report.Dll"
    },
    "Dictionary": {
        "DataSources": {
            "0": {
                "Ident": "StiDataTableSource",
                "Name": "orderinfo",
                "Alias": "orderinfo",
                "Columns": {
                    "0": {
                        "Name": "id",
                        "Index": -1,
                        "NameInSource": "id",
                        "Alias": "id",
                        "Type": "System.String"
                    },
                    "1": {
                        "Name": "username",
                        "Index": -1,
                        "NameInSource": "username",
                        "Alias": "username",
                        "Type": "System.String"
                    },
                    "2": {
                        "Name": "sodoordate",
                        "Index": -1,
                        "NameInSource": "sodoordate",
                        "Alias": "sodoordate",
                        "Type": "System.String"
                    },
                    "3": {
                        "Name": "sodoortime",
                        "Index": -1,
                        "NameInSource": "sodoortime",
                        "Alias": "sodoortime",
                        "Type": "System.String"
                    },
                    "4": {
                        "Name": "fullname",
                        "Index": -1,
                        "NameInSource": "fullname",
                        "Alias": "fullname",
                        "Type": "System.String"
                    },
                    "5": {
                        "Name": "cellphone",
                        "Index": -1,
                        "NameInSource": "cellphone",
                        "Alias": "cellphone",
                        "Type": "System.String"
                    },
                    "6": {
                        "Name": "frombranch",
                        "Index": -1,
                        "NameInSource": "frombranch",
                        "Alias": "frombranch",
                        "Type": "System.String"
                    },
                    "7": {
                        "Name": "tobranch",
                        "Index": -1,
                        "NameInSource": "tobranch",
                        "Alias": "tobranch",
                        "Type": "System.String"
                    },
                    "8": {
                        "Name": "sharh",
                        "Index": -1,
                        "NameInSource": "sharh",
                        "Alias": "sharh",
                        "Type": "System.String"
                    },
                    "9": {
                        "Name": "done",
                        "Index": -1,
                        "NameInSource": "done",
                        "Alias": "done",
                        "Type": "System.String"
                    },
                    "10": {
                        "Name": "address",
                        "Index": -1,
                        "NameInSource": "address",
                        "Alias": "address",
                        "Type": "System.String"
                    },
                    "11": {
                        "Name": "frombranch_address",
                        "Index": -1,
                        "NameInSource": "frombranch_address",
                        "Alias": "frombranch_address",
                        "Type": "System.String"
                    },
                    "12": {
                        "Name": "tobranch_address",
                        "Index": -1,
                        "NameInSource": "tobranch_address",
                        "Alias": "tobranch_address",
                        "Type": "System.String"
                    }
                },
                "NameInSource": "orderinfo"
            },
            "1": {
                "Ident": "StiDataTableSource",
                "Name": "orderitems",
                "Alias": "orderitems",
                "Columns": {
                    "0": {
                        "Name": "productid",
                        "Index": -1,
                        "NameInSource": "productid",
                        "Alias": "productid",
                        "Type": "System.String"
                    },
                    "1": {
                        "Name": "productname",
                        "Index": -1,
                        "NameInSource": "productname",
                        "Alias": "productname",
                        "Type": "System.String"
                    },
                    "2": {
                        "Name": "weight",
                        "Index": -1,
                        "NameInSource": "weight",
                        "Alias": "weight",
                        "Type": "System.String"
                    },
                    "3": {
                        "Name": "number",
                        "Index": -1,
                        "NameInSource": "number",
                        "Alias": "number",
                        "Type": "System.String"
                    },
                    "4": {
                        "Name": "Catgname",
                        "Index": -1,
                        "NameInSource": "Catgname",
                        "Alias": "Catgname",
                        "Type": "System.String"
                    },
                    "5": {
                        "Name": "desc",
                        "Index": -1,
                        "NameInSource": "desc",
                        "Alias": "desc",
                        "Type": "System.String"
                    },
                    "6": {
                        "Name": "totalweight",
                        "Index": -1,
                        "NameInSource": "totalweight",
                        "Alias": "totalweight",
                        "Type": "System.String"
                    }
                },
                "NameInSource": "orderitems"
            },
            "2": {
                "Ident": "StiDataTableSource",
                "Name": "abstractinfo",
                "Alias": "abstractinfo",
                "Columns": {
                    "0": {
                        "Name": "totalprice",
                        "Index": -1,
                        "NameInSource": "totalprice",
                        "Alias": "totalprice",
                        "Type": "System.String"
                    },
                    "1": {
                        "Name": "discount",
                        "Index": -1,
                        "NameInSource": "discount",
                        "Alias": "discount",
                        "Type": "System.String"
                    },
                    "2": {
                        "Name": "totalcosttransportation",
                        "Index": -1,
                        "NameInSource": "totalcosttransportation",
                        "Alias": "totalcosttransportation",
                        "Type": "System.String"
                    },
                    "3": {
                        "Name": "payableprice",
                        "Index": -1,
                        "NameInSource": "payableprice",
                        "Alias": "payableprice",
                        "Type": "System.String"
                    },
                    "4": {
                        "Name": "totalweight",
                        "Index": -1,
                        "NameInSource": "totalweight",
                        "Alias": "totalweight",
                        "Type": "System.String"
                    }
                },
                "NameInSource": "abstractinfo"
            },
            "3": {
                "Ident": "StiDataTableSource",
                "Name": "transportationinfo",
                "Alias": "transportationinfo",
                "Columns": {
                    "0": {
                        "Name": "locationname",
                        "Index": -1,
                        "NameInSource": "locationname",
                        "Alias": "locationname",
                        "Type": "System.String"
                    },
                    "1": {
                        "Name": "Address",
                        "Index": -1,
                        "NameInSource": "Address",
                        "Alias": "Address",
                        "Type": "System.String"
                    },
                    "2": {
                        "Name": "personname",
                        "Index": -1,
                        "NameInSource": "personname",
                        "Alias": "personname",
                        "Type": "System.String"
                    },
                    "3": {
                        "Name": "phonenumber",
                        "Index": -1,
                        "NameInSource": "phonenumber",
                        "Alias": "phonenumber",
                        "Type": "System.String"
                    },
                    "4": {
                        "Name": "distance",
                        "Index": -1,
                        "NameInSource": "distance",
                        "Alias": "distance",
                        "Type": "System.String"
                    }
                }
            }
        }
    },
    "Pages": {
        "0": {
            "Ident": "StiPage",
            "Name": "Page1",
            "Guid": "dc534c3640d345aa95d2286dbb46b4a0",
            "Interaction": {
                "Ident": "StiInteraction"
            },
            "Border": ";;2;;;;;solid:Black",
            "Brush": "solid:",
            "Components": {
                "0": {
                    "Ident": "StiPageHeaderBand",
                    "Name": "PageHeaderBand1",
                    "ClientRectangle": "0,0.2,5.43,0.5",
                    "Interaction": {
                        "Ident": "StiInteraction"
                    },
                    "Border": ";;;;;;;solid:Black",
                    "Brush": "solid:",
                    "Components": {
                        "0": {
                            "Ident": "StiText",
                            "Name": "Text2",
                            "Guid": "bdb28294b52343e0ab597c99b031e6a0",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "1.8,0,2.3,0.4",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "کالای چوب سلطانی"
                            },
                            "VertAlignment": "Center",
                            "Font": "B Zar;16;Bold;",
                            "Border": ";;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "Type": "Expression"
                        },
                        "1": {
                            "Ident": "StiText",
                            "Name": "Text55",
                            "Guid": "0333c94147f142e0b7b1ba1368000886",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,0,1.2,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{orderinfo.sodoordate}"
                            },
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;;",
                            "Border": ";;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:15,36,62",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "2": {
                            "Ident": "StiText",
                            "Name": "Text56",
                            "Guid": "89036ab98a9748e8a9034719f59cb348",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,0.2,1.2,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{orderinfo.sodoortime}"
                            },
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;;",
                            "Border": ";;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:15,36,62",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "3": {
                            "Ident": "StiText",
                            "Name": "Text37",
                            "Guid": "975aea56183b4aada6662469668040e1",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "1.3,0.17,2.2,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "user : {orderinfo.username}"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;;",
                            "Border": ";;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:15,36,62",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "4": {
                            "Ident": "StiText",
                            "Name": "Text11",
                            "Guid": "cd023b19b09a4826a9c563ccbf676a08",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "2.6,0.1,2.5,0.4",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "حواله انبار : {orderinfo.id}"
                            },
                            "HorAlignment": "Right",
                            "VertAlignment": "Center",
                            "Font": "B Zar;16;Bold;",
                            "Border": ";;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:192,0,0",
                            "Type": "Expression"
                        },
                        "5": {
                            "Ident": "StiHorizontalLinePrimitive",
                            "Name": "HorizontalLinePrimitive1",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,0.5,5.2,0.01",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Style": "Double",
                            "StartCap": ";;;",
                            "EndCap": ";;;"
                        }
                    }
                },
                "1": {
                    "Ident": "StiPageFooterBand",
                    "Name": "PageFooterBand1",
                    "ClientRectangle": "0,8.64,5.43,0.8",
                    "Interaction": {
                        "Ident": "StiInteraction"
                    },
                    "Border": ";;;;;;;solid:Black",
                    "Brush": "solid:",
                    "Components": {
                        "0": {
                            "Ident": "StiText",
                            "Name": "Text16",
                            "Guid": "f6967eb14974474fae51b898586db838",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "1.1,0.36,4.3,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "آدرس {orderinfo.tobranch} : {orderinfo.tobranch_address}"
                            },
                            "HorAlignment": "Right",
                            "Font": "Tahoma;10;Bold;",
                            "Border": ";;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:0,32,96",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "1": {
                            "Ident": "StiText",
                            "Name": "Text15",
                            "Guid": "e35e88b132f247229b1cd688023333c5",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "1.1,0.06,4.3,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "آدرس  {orderinfo.frombranch}:{orderinfo.frombranch_address}"
                            },
                            "HorAlignment": "Right",
                            "Font": "Tahoma;10;Bold;",
                            "Border": ";;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:0,32,96",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "2": {
                            "Ident": "StiText",
                            "Name": "Text8",
                            "Guid": "9cced39f90d74b6badd7c67b8dd69756",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,0.36,0.6,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{PageNumber}"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;10;;",
                            "Border": ";;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:0,32,96",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        }
                    }
                },
                "2": {
                    "Ident": "StiReportTitleBand",
                    "Name": "ReportTitleBand1",
                    "ClientRectangle": "0,1.1,5.43,1.4",
                    "Interaction": {
                        "Ident": "StiInteraction"
                    },
                    "Border": ";;;;;;;solid:Black",
                    "Brush": "solid:",
                    "Components": {
                        "0": {
                            "Ident": "StiText",
                            "Name": "Text5",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,1,1.7,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{orderinfo.done}"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;10;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:192,0,0",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "1": {
                            "Ident": "StiText",
                            "Name": "Text6",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,0.7,1.7,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "وضعیت تحویل"
                            },
                            "HorAlignment": "Right",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "2": {
                            "Ident": "StiText",
                            "Name": "Text3",
                            "Guid": "59dcee40b7a94626ac4cf02cbe5847ce",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "2.8,0.1,1.5,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{orderinfo.frombranch}"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;10;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:192,0,0",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "DataColumn"
                        },
                        "3": {
                            "Ident": "StiText",
                            "Name": "Text4",
                            "Guid": "b15120a9f80240e7baa1a242dd8587d4",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "4.3,0.4,1,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "به شعبه"
                            },
                            "HorAlignment": "Right",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "4": {
                            "Ident": "StiText",
                            "Name": "Text10",
                            "Guid": "8d4c1937c22e4c5794b45c52e06274db",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "2.8,0.4,1.5,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{orderinfo.tobranch}"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;10;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:192,0,0",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "DataColumn"
                        },
                        "5": {
                            "Ident": "StiText",
                            "Name": "Text42",
                            "Guid": "2177e4b8a1534800bd2db2ead0801e75",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "1.8,0.4,1,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": " موبایل"
                            },
                            "HorAlignment": "Right",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "6": {
                            "Ident": "StiText",
                            "Name": "Text48",
                            "Guid": "c1c3954bcd5e4aa5b62be708f0cf3bdc",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "1.8,0.1,1,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": " نام مشتری"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "7": {
                            "Ident": "StiText",
                            "Name": "Text1",
                            "Guid": "3ee7f5c7786648bf8c1ae7e3671a6cae",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "4.3,0.1,1,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "از شعبه"
                            },
                            "HorAlignment": "Right",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "8": {
                            "Ident": "StiText",
                            "Name": "Text41",
                            "Guid": "91fa3cfa27544e968a322d58e7badc2d",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,0.4,1.7,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{orderinfo.cellphone}"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;10;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:192,0,0",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "9": {
                            "Ident": "StiText",
                            "Name": "Text39",
                            "Guid": "a19ad6a6922f4cdf9792422ac74b67a5",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "1.8,0.7,2.5,0.6",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{orderinfo.address}"
                            },
                            "HorAlignment": "Right",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;10;;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:192,0,0",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "10": {
                            "Ident": "StiText",
                            "Name": "Text40",
                            "Guid": "cc0fec9064954dabaa6aad16e57f2cfb",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "4.3,0.7,1,0.6",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": " آدرس "
                            },
                            "HorAlignment": "Right",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "11": {
                            "Ident": "StiText",
                            "Name": "Text47",
                            "Guid": "1b9e31ac31784590bbadcc26267079cf",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,0.1,1.7,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{orderinfo.fullname}"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;10;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:192,0,0",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        }
                    }
                },
                "3": {
                    "Ident": "StiColumnHeaderBand",
                    "Name": "ColumnHeaderBand1",
                    "ClientRectangle": "0,2.9,5.43,0.3",
                    "Interaction": {
                        "Ident": "StiInteraction"
                    },
                    "Border": ";;;;;;;solid:Black",
                    "Brush": "solid:",
                    "Components": {
                        "0": {
                            "Ident": "StiText",
                            "Name": "Text19",
                            "Guid": "77e635178647486fa3caad3ebd5125c4",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "1.6,0,3.4,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "نام کالا"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:146,208,80",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "1": {
                            "Ident": "StiText",
                            "Name": "Text20",
                            "Guid": "4e3726b01dbc4c7e8b02deee1949ba33",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.9,0,0.7,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "مقدار"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:146,208,80",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "2": {
                            "Ident": "StiText",
                            "Name": "Text22",
                            "Guid": "cc9d5d0ac42d4682a70f987f7612925f",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,0,0.8,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "کل وزن"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:146,208,80",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "3": {
                            "Ident": "StiText",
                            "Name": "Text17",
                            "Guid": "d4a54e42cc374883ac0cca578844f71b",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "5,0,0.3,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "#"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:146,208,80",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        }
                    }
                },
                "4": {
                    "Ident": "StiDataBand",
                    "Name": "DataBand1",
                    "ClientRectangle": "0,3.6,5.43,0.5",
                    "Interaction": {
                        "Ident": "StiBandInteraction"
                    },
                    "Border": ";;;;;;;solid:Black",
                    "Brush": "solid:",
                    "Components": {
                        "0": {
                            "Ident": "StiText",
                            "Name": "Text25",
                            "Guid": "91db073623f34c589422663f2361869d",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "1.6,0,3.4,0.5",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{orderitems.productname}"
                            },
                            "VertAlignment": "Center",
                            "Font": "Tahoma;10;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "TextOptions": {
                                "RightToLeft": true,
                                "WordWrap": true
                            },
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "DataColumn"
                        },
                        "1": {
                            "Ident": "StiText",
                            "Name": "Text26",
                            "Guid": "3f8ab287c43e4275a0787ee1a01a973d",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.9,0,0.7,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{orderitems.number}"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;10;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "2": {
                            "Ident": "StiText",
                            "Name": "Text28",
                            "Guid": "d0396610bb2e40babd208e3e685b93f5",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,0,0.8,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{orderitems.totalweight}  kg "
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;10;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "3": {
                            "Ident": "StiText",
                            "Name": "Text29",
                            "Guid": "49c9d591b1794f0c9b7127a17577e878",
                            "GrowToHeight": true,
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,0.3,1.5,0.2",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{orderitems.desc}"
                            },
                            "VertAlignment": "Center",
                            "Font": "Tahoma;;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "TextOptions": {
                                "RightToLeft": true
                            },
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "4": {
                            "Ident": "StiText",
                            "Name": "Text23",
                            "Guid": "e917b07f731441eda1ed649b45e93a1c",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "5,0,0.3,0.5",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{Line}"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;10;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        }
                    },
                    "DataSourceName": "orderitems"
                },
                "5": {
                    "Ident": "StiColumnFooterBand",
                    "Name": "ColumnFooterBand1",
                    "ClientRectangle": "0,4.5,5.43,1.8",
                    "Interaction": {
                        "Ident": "StiInteraction"
                    },
                    "Border": ";;;;;;;solid:Black",
                    "Brush": "solid:",
                    "Components": {
                        "0": {
                            "Ident": "StiText",
                            "Name": "Text33",
                            "Guid": "0c943113bdf74f0188fcc49f7bef8116",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.9,0.1,1.5,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "وزن کل - کیلوگرم"
                            },
                            "HorAlignment": "Right",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "1": {
                            "Ident": "StiText",
                            "Name": "Text34",
                            "Guid": "4715025e559247fb802e73afbcb3b0c9",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,0.1,0.8,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{Sum(orderitems.totalweight)}"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;12;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "2": {
                            "Ident": "StiText",
                            "Name": "Text31",
                            "Guid": "761cd089027847b783572c0d3b82e6ec",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.9,0.4,1.5,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "تعداد کل"
                            },
                            "HorAlignment": "Right",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;14;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:Black",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "3": {
                            "Ident": "StiText",
                            "Name": "Text43",
                            "Guid": "a8d2bddad9624d379b2592442cbba4e4",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,0.4,0.8,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "{Sum(orderitems.number)}"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;14;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:192,0,0",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "4": {
                            "Ident": "StiText",
                            "Name": "Text7",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "2.4,0.1,2.9,0.6",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "توضیحات\r\n{orderinfo.sharh}"
                            },
                            "HorAlignment": "Right",
                            "Font": "Tahoma;10;Bold;",
                            "Border": "All;;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:0,32,96",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "5": {
                            "Ident": "StiText",
                            "Name": "Text12",
                            "Guid": "39a33653a8f047b6a882e241655acd77",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "3.2,1.1,2.1,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "تحویل دهنده"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;10;Bold;",
                            "Border": ";;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:0,32,96",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "6": {
                            "Ident": "StiText",
                            "Name": "Text13",
                            "Guid": "ae7a31c10a0e4ce19c94e74a01678f33",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,1.1,2.3,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "تحویل گیرنده"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Tahoma;10;Bold;",
                            "Border": ";;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:0,32,96",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "7": {
                            "Ident": "StiText",
                            "Name": "Text14",
                            "Guid": "92cb05b397bd4a6fb42eef402387f531",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "0.1,0.8,5.2,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": "اقلام مذکور به طور صیحیح و سالم تحویل گرفته شد"
                            },
                            "HorAlignment": "Right",
                            "Font": "Tahoma;10;Bold;",
                            "Border": ";;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:0,32,96",
                            "Margins": {
                                "Left": 5.0,
                                "Right": 5.0,
                                "Top": 5.0,
                                "Bottom": 5.0
                            },
                            "Type": "Expression"
                        },
                        "8": {
                            "Ident": "StiText",
                            "Name": "Text9",
                            "Guid": "5d72389d1b1a401485885024bdb79311",
                            "MinSize": "0,0",
                            "MaxSize": "0,0",
                            "ClientRectangle": "1.2,1.5,3.3,0.3",
                            "Interaction": {
                                "Ident": "StiInteraction"
                            },
                            "Text": {
                                "Value": " www.soltaniwoodproducts.com"
                            },
                            "HorAlignment": "Center",
                            "VertAlignment": "Center",
                            "Font": "Book Antiqua;10;Bold;",
                            "Border": ";;;;;;;solid:Black",
                            "Brush": "solid:",
                            "TextBrush": "solid:47,54,153",
                            "Type": "Expression"
                        }
                    }
                }
            },
            "PaperSize": "A5",
            "PageWidth": 5.83,
            "PageHeight": 8.27,
            "Watermark": {
                "TextBrush": "solid:50,0,0,0"
            },
            "Margins": {
                "Left": 0.2,
                "Right": 0.2,
                "Top": 0.2,
                "Bottom": 0.2
            }
        }
    }
}
