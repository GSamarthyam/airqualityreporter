package com.mycompany.reporting;

import com.itextpdf.text.DocumentException;
import com.mycompany.fileprocessors.PDFDocument;

import java.time.LocalDateTime;
import java.util.List;
import java.util.Map;
import java.util.function.BiConsumer;

/* Abstraction responsible for generating a PDF report given the filename, heading,
   date & time of the feed, and the pollution-data tables
 */
public class PDFReport {
    private String heading;
    private LocalDateTime dateTime;
    public PDFReport(String heading, LocalDateTime dateTime) {
        this.heading = heading;
        this.dateTime = dateTime;
    }

    public void generate(String fileName, Map<String, List<String>> entries,
                                   List<String> columnHeaders) throws Exception {
        try (PDFDocument report = new PDFDocument(fileName)) {
            report.addTitle(heading);
            report.addHeader("Generated at: " + dateTime);
            PDFDocument finalReport = report;
            entries.forEach(addTableToReport(columnHeaders, finalReport));
        }
    }

    private BiConsumer<String, List<String>> addTableToReport(List<String> columnHeaders, PDFDocument finalReport) {
        return (tableTitle, values) -> {
            try {
                finalReport.addTable(tableTitle, columnHeaders, values);
            } catch (DocumentException e) {
                e.printStackTrace();
            }
        };
    }
}
