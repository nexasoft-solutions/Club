using System.Globalization;
using NexaSoft.Club.Application.Abstractions.Reporting;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Queries.GetReceiptPeriodByEmployee;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NexaSoft.Club.Infrastructure.Repositories.Reports;

public class PayrollReceiptService : IPayrollReceiptService
{
    public byte[] GeneratePayrollReceipt(PayrollPeriodItemResponse payroll, Action<PayrollReceiptConfig>? configure = null)
    {
        var config = GetDefaultConfiguration();
        configure?.Invoke(config);

        return GenerateReceipt(payroll, config);
    }

    public byte[] GenerateEmployeePayrollReceipt(PayrollDetailItemsResponse employeeDetail, Action<PayrollReceiptConfig>? configure = null)
    {
        var payroll = new PayrollPeriodItemResponse
        (
            1,
            "BOLETA INDIVIDUAL",
            1,
            "QUINCENAL",
            DateOnly.FromDateTime(DateTime.Now.AddDays(-15)),
            DateOnly.FromDateTime(DateTime.Now),
            employeeDetail.NetPay,
            1,
            0,
            DateTime.Now,
            "SISTEMA",
            new List<PayrollDetailItemsResponse> { employeeDetail }
        );

        var config = GetDefaultConfiguration();
        configure?.Invoke(config);
        config.ShowFooter = false;

        return GenerateReceipt(payroll, config);
    }

    public byte[] GenerateA4PayrollReceipt(PayrollPeriodItemResponse payroll)
    {
        return GeneratePayrollReceipt(payroll, config =>
        {
            config.PageSize = PageSizes.A4;
            config.ShowLogo = true;
            config.ShowCompanyDetails = true;
            config.ShowBreakdown = true;
        });
    }

    public byte[] GenerateA5PayrollReceipt(PayrollPeriodItemResponse payroll)
    {
        return GeneratePayrollReceipt(payroll, config =>
        {
            config.PageSize = PageSizes.A5;
            config.FontSizeNormal = 8;
            config.FontSizeSmall = 7;
            config.FontSizeLarge = 10;
            config.ShowLogo = false;
        });
    }

    public PayrollReceiptConfig GetDefaultConfiguration()
    {
        return new PayrollReceiptConfig();
    }

    private byte[] GenerateReceipt(PayrollPeriodItemResponse payroll, PayrollReceiptConfig config)
    {
        var document = Document.Create(container =>
        {
            foreach (var employee in payroll.Details)
            {
                container.Page(page =>
                {
                    // Media página A4 (mitad vertical)
                    page.Size(PageSizes.A4);
                    page.Margin(15);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(config.FontSizeNormal).FontFamily("Arial"));

                    // Header con información de la empresa
                    if (config.ShowHeader)
                    {
                        page.Header().Element(compose => ComposeHeader(compose, payroll, config));
                    }

                    // Contenido principal - Boleta del empleado
                    page.Content().Element(compose => ComposeEmployeeContent(compose, employee, payroll, config));

                    // Footer con información legal
                    if (config.ShowFooter)
                    {
                        page.Footer().Element(compose => ComposeFooter(compose, config));
                    }
                });
            }
        });

        return document.GeneratePdf();
    }

    private void ComposeHeader(IContainer container, PayrollPeriodItemResponse payroll, PayrollReceiptConfig config)
    {
        container.Column(column =>
        {
            // Logo y nombre de la empresa
            column.Item().Row(row =>
            {
                if (config.ShowLogo)
                {
                    row.ConstantItem(40).Height(40).Background(config.PrimaryColor).AlignCenter().AlignMiddle().Text("NS").Bold().FontColor(Colors.White);
                }

                row.RelativeItem().PaddingLeft(10).Column(col =>
                {
                    col.Item().Text(config.CompanyName).Bold().FontSize(config.FontSizeLarge).FontColor(config.PrimaryColor);
                    col.Item().Text(config.CompanyAddress).FontSize(config.FontSizeSmall);
                    col.Item().Text($"RUC: {config.CompanyRUC}").FontSize(config.FontSizeSmall);
                });
            });

            // Línea separadora
            column.Item().PaddingTop(5).LineHorizontal(1).LineColor(config.PrimaryColor);

            // Información del período de planilla
            column.Item().PaddingTop(8).Row(headerRow =>
            {
                headerRow.RelativeItem().Column(col =>
                {
                    col.Item().Text("BOLETA DE PAGO").Bold().FontSize(config.FontSizeLarge);
                    col.Item().Text($"Período: {payroll.PeriodName}").SemiBold();
                });

                headerRow.RelativeItem().AlignRight().Column(col =>
                {
                    col.Item().Text($"Del: {payroll.StartDate:dd/MM/yyyy}").FontSize(config.FontSizeSmall);
                    col.Item().Text($"Al: {payroll.EndDate:dd/MM/yyyy}").FontSize(config.FontSizeSmall);
                });
            });
        });
    }

    private void ComposeEmployeeContent(IContainer container, PayrollDetailItemsResponse employee,
        PayrollPeriodItemResponse payroll, PayrollReceiptConfig config)
    {
        container.Column(column =>
        {
            // Información del empleado - SIMPLIFICADA
            column.Item().PaddingVertical(10).Background(Colors.Blue.Lighten5).Padding(10)
                .Border(1).BorderColor(Colors.Blue.Lighten2).Column(empCol =>
            {
                // Nombre centrado
                empCol.Item().AlignCenter().Text(employee.EmployeeFullName.ToUpper())
                    .Bold().FontSize(config.FontSizeLarge).FontColor(config.PrimaryColor);

                // Primera línea de datos
                empCol.Item().AlignCenter().PaddingTop(4)
                    .Text($"DNI: {employee.EmployeeDni}   |   Código: {employee.EmployeeCode}   |   Puesto: {employee.EmployeePositionCode}").Bold();

                // Segunda línea de datos  
                empCol.Item().AlignCenter().PaddingTop(2)
                    .Text($"Contratación: {employee.EmployeeHireDate:dd/MM/yyyy}   |   Departamento: {employee.EmployeeDepartmentCode}").Bold();
            });

            // Separador
            column.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

            // INGRESOS - Diseño de tabla mejorado
            column.Item().PaddingVertical(8).Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3); // Concepto
                    columns.ConstantColumn(100); // Monto
                });

                // Encabezado INGRESOS
                table.Cell().ColumnSpan(2).PaddingBottom(5).Text("INGRESOS")
                    .Bold().FontSize(config.FontSizeNormal).FontColor(config.PrimaryColor);

                var ingresos = employee.Concepts!.Where(c => IsIncomeConcept(c)).ToList();
                foreach (var concepto in ingresos)
                {
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(3)
                        .Text(concepto.ConceptName);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(3)
                        .AlignRight().Text(concepto.CalculatedValue.ToString("C", CultureInfo.GetCultureInfo("es-PE")));
                }

                // Total Ingresos
                table.Cell().BorderBottom(2).BorderColor(Colors.Grey.Medium).PaddingVertical(4)
                    .Text("TOTAL INGRESOS").Bold();
                table.Cell().BorderBottom(2).BorderColor(Colors.Grey.Medium).PaddingVertical(4)
                    .AlignRight().Text(employee.TotalIncome.ToString("C", CultureInfo.GetCultureInfo("es-PE"))).Bold();

                // Espacio entre secciones
                table.Cell().ColumnSpan(2).PaddingVertical(10);

                // Encabezado DESCUENTOS
                table.Cell().ColumnSpan(2).PaddingBottom(5).Text("DESCUENTOS")
                    .Bold().FontSize(config.FontSizeNormal).FontColor(Colors.Red.Medium);

                var descuentos = employee.Concepts!.Where(c => IsDeductionConcept(c)).ToList();
                foreach (var concepto in descuentos)
                {
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(3)
                        .Text(concepto.ConceptName);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(3)
                        .AlignRight().Text(concepto.CalculatedValue.ToString("C", CultureInfo.GetCultureInfo("es-PE")));
                }

                // Total Descuentos
                table.Cell().BorderBottom(2).BorderColor(Colors.Grey.Medium).PaddingVertical(4)
                    .Text("TOTAL DESCUENTOS").Bold();
                table.Cell().BorderBottom(2).BorderColor(Colors.Grey.Medium).PaddingVertical(4)
                    .AlignRight().Text(employee.TotalDeductions.ToString("C", CultureInfo.GetCultureInfo("es-PE"))).Bold();
            });

            column.Item().PaddingVertical(8).LineHorizontal(2).LineColor(config.AccentColor);

            // NETO A PAGAR - Destacado
            column.Item().PaddingVertical(5).Background(config.AccentColor).Padding(10).Row(row =>
            {
                row.RelativeItem().Text("NETO A PAGAR").Bold().FontSize(config.FontSizeLarge).FontColor(Colors.White);
                row.ConstantItem(120).AlignRight().Text(employee.NetPay.ToString("C", CultureInfo.GetCultureInfo("es-PE")))
                    .Bold().FontSize(config.FontSizeLarge).FontColor(Colors.White);
            });

            // Monto en letras - CON RECUADRO pero mejorado
            column.Item().PaddingTop(8).Background(Colors.Grey.Lighten4).Padding(8)
                .Border(1).BorderColor(Colors.Grey.Lighten2).Column(letras =>
            {
                letras.Item().AlignCenter().Text("SON:").SemiBold().FontSize(config.FontSizeSmall);
                letras.Item().AlignCenter().PaddingTop(2).Text(ConvertAmountToText(employee.NetPay))
                    .Italic().FontSize(config.FontSizeSmall);
            });

            // Desglose detallado (opcional) - Mejor organizado
            /*if (config.ShowBreakdown && employee.Concepts!.Any())
            {
                column.Item().PaddingVertical(10).Column(breakdown =>
                {
                    breakdown.Item().AlignCenter().Text("DETALLE DE CONCEPTOS")
                        .SemiBold().FontSize(config.FontSizeSmall).FontColor(Colors.Grey.Darken1);

                    // Agrupar por tipo de concepto
                    var groupedConcepts = employee.Concepts!
                        .GroupBy(c => IsIncomeConcept(c) ? "INGRESO" : "DESCUENTO")
                        .OrderBy(g => g.Key == "DESCUENTO"); // Ingresos primero

                    foreach (var group in groupedConcepts)
                    {
                        breakdown.Item().PaddingTop(5).Text($"{group.Key}S:").SemiBold().FontSize(config.FontSizeSmall - 1);

                        foreach (var concepto in group)
                        {
                            breakdown.Item().PaddingLeft(20).Row(row =>
                            {
                                row.RelativeItem().Text($"• {concepto.ConceptName}").FontSize(config.FontSizeSmall - 1);
                                row.ConstantItem(80).AlignRight().Text(concepto.CalculatedValue.ToString("C", CultureInfo.GetCultureInfo("es-PE")))
                                    .FontSize(config.FontSizeSmall - 1);
                            });
                        }
                    }
                });
            }*/

            // Firma y sello
              column.Item().PaddingTop(15).Row(firma =>
            {
                firma.RelativeItem().Column(col =>
                {
                    col.Item().LineHorizontal(1).LineColor(Colors.Black);
                    col.Item().AlignCenter().Text("Firma del Trabajador").FontSize(config.FontSizeSmall);
                });
                
                firma.RelativeItem().Column(col =>
                {
                    col.Item().LineHorizontal(1).LineColor(Colors.Black);
                    col.Item().AlignCenter().Text("Firma y Sello del Empleador").FontSize(config.FontSizeSmall);
                });
            });        
        });
    }

    private void ComposeFooter(IContainer container, PayrollReceiptConfig config)
    {
        container.Column(column =>
        {
            column.Item().PaddingTop(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
            column.Item().PaddingTop(5).AlignCenter().Text($"{config.CompanyPhone} | {config.CompanyEmail}")
                .FontSize(config.FontSizeSmall).FontColor(Colors.Grey.Medium);

            if (config.ShowLegalText)
            {
                column.Item().PaddingTop(3).AlignCenter().Text("Documento generado electrónicamente - Válido sin firma ni sello")
                    .FontSize(config.FontSizeSmall - 1).FontColor(Colors.Grey.Medium);
            }

            column.Item().AlignCenter().Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}")
                .FontSize(config.FontSizeSmall - 1).FontColor(Colors.Grey.Medium);
        });
    }

    // Métodos auxiliares para determinar tipo de concepto
    private bool IsIncomeConcept(PayrollDetailConceptItemsResponse concept)
    {
        return concept.ConceptType?.ToLower() == "ingreso" ||
               concept.Amount >= 0 && !IsDeductionByCode(concept.ConceptCode);
    }

    private bool IsDeductionConcept(PayrollDetailConceptItemsResponse concept)
    {
        return concept.ConceptType?.ToLower() == "descuento" ||
               concept.Amount < 0 || IsDeductionByCode(concept.ConceptCode);
    }

    private bool IsDeductionByCode(string conceptCode)
    {
        var deductionCodes = new[] { "AFP", "SALUD", "TARDANZA", "DESC", "DSCTO" };
        return deductionCodes.Any(code => conceptCode?.ToUpper().Contains(code) == true);
    }

    private string ConvertAmountToText(decimal amount)
    {
        if (amount == 0) return "CERO CON 00/100 SOLES";

        var integerPart = (int)Math.Truncate(amount);
        var decimalPart = (int)((amount - integerPart) * 100);

        var integerText = ConvertIntegerToText(integerPart);
        return $"{integerText} CON {decimalPart:00}/100 SOLES";
    }

    private string ConvertIntegerToText(int number)
    {
        if (number == 0) return "CERO";
        if (number < 0) return "MENOS " + ConvertIntegerToText(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += ConvertIntegerToText(number / 1000000) + " MILLÓN ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += ConvertHundreds(number / 1000) + " MIL ";
            number %= 1000;
        }

        if (number > 0)
        {
            words += ConvertHundreds(number);
        }

        return words.Trim();
    }

    private string ConvertHundreds(int number)
    {
        string words = "";

        if (number > 99)
        {
            words = number / 100 == 1 ? "CIEN" : ConvertUnits(number / 100) + "CIENTOS";
            number %= 100;
            if (number > 0) words += " ";
        }

        if (number > 0)
        {
            if (number < 10)
            {
                words += ConvertUnits(number);
            }
            else if (number < 20)
            {
                words += ConvertTeens(number);
            }
            else
            {
                words += ConvertTens(number / 10);
                if ((number % 10) > 0)
                {
                    words += " Y " + ConvertUnits(number % 10).ToLower();
                }
            }
        }

        return words;
    }

    private string ConvertUnits(int number)
    {
        return number switch
        {
            1 => "UNO",
            2 => "DOS",
            3 => "TRES",
            4 => "CUATRO",
            5 => "CINCO",
            6 => "SEIS",
            7 => "SIETE",
            8 => "OCHO",
            9 => "NUEVE",
            _ => ""
        };
    }

    private string ConvertTeens(int number)
    {
        return number switch
        {
            10 => "DIEZ",
            11 => "ONCE",
            12 => "DOCE",
            13 => "TRECE",
            14 => "CATORCE",
            15 => "QUINCE",
            16 => "DIECISÉIS",
            17 => "DIECISIETE",
            18 => "DIECIOCHO",
            19 => "DIECINUEVE",
            _ => ""
        };
    }

    private string ConvertTens(int number)
    {
        return number switch
        {
            2 => "VEINTE",
            3 => "TREINTA",
            4 => "CUARENTA",
            5 => "CINCUENTA",
            6 => "SESENTA",
            7 => "SETENTA",
            8 => "OCHENTA",
            9 => "NOVENTA",
            _ => ""
        };
    }
}