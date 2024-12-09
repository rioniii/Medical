import React, { useState, useEffect } from 'react';
import { jsPDF } from 'jspdf';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';

const Invoice = ({ patientId }) => {
    const [invoiceData, setInvoiceData] = useState({
        clinic: {
            name: 'Delight Dental Clinic',
            email: 'delightdental@gmail.com',
            phone: '+ (456) 768, 972, 90',
        },
        patient: {},
        services: [],
        subTotal: 0,
        discount: 0,
        tax: 0,
        grandTotal: 0,
        currency: 'USD ($)',
        date: new Date().toLocaleDateString(),
        dueDate: '',
        invoiceNumber: '#206719',
    });

    useEffect(() => {
        // Fetch invoice data for the selected patient
        const fetchInvoiceData = async () => {
            try {
                const response = await axios.get(`/api/patients/${patientId}/invoice`);
                const patientData = response.data.patient;
                const services = response.data.services;

                const subTotal = services.reduce((total, service) => total + (service.price * service.quantity), 0);
                const discount = response.data.discount || 0;
                const tax = response.data.tax || 0;
                const grandTotal = subTotal - discount + tax;

                setInvoiceData({
                    ...invoiceData,
                    patient: patientData,
                    services,
                    subTotal,
                    discount,
                    tax,
                    grandTotal,
                    dueDate: new Date(new Date().setDate(new Date().getDate() + 7)).toLocaleDateString(),
                });
            } catch (error) {
                console.error("Error fetching invoice data:", error);
            }
        };

        if (patientId) {
            fetchInvoiceData();
        }
    }, [patientId]);

    const generatePDF = () => {
        const doc = new jsPDF();
        // Add content to PDF as before...
        doc.save(`invoice-${invoiceData.invoiceNumber}.pdf`);
    };

    return (
        <div className="container mt-5">
            <Sidebar userType="doctor" />

            <div className="row">
                <div className="col-md-12">
                    <h4 className="text-center">Preview Invoice</h4>
                    {/* Render the invoice details */}
                    <div className="invoice-header d-flex justify-content-between mb-4">
                        <div className="clinic-info">
                            <h5>{invoiceData.clinic.name}</h5>
                            <p>{invoiceData.clinic.email}</p>
                            <p>{invoiceData.clinic.phone}</p>
                        </div>
                        <div className="patient-info text-right">
                            <h5>To:</h5>
                            <p>{invoiceData.patient.name}</p>
                            <p>{invoiceData.patient.email}</p>
                            <p>{invoiceData.patient.phone}</p>
                        </div>
                    </div>

                    <div className="invoice-details">
                        <p><strong>Invoice #:</strong> {invoiceData.invoiceNumber}</p>
                        <p><strong>Date:</strong> {invoiceData.date}</p>
                        <p><strong>Due Date:</strong> {invoiceData.dueDate}</p>
                    </div>

                    <table className="table table-bordered mt-4">
                        <thead>
                            <tr>
                                <th>Item</th>
                                <th>Item Price (Tsh)</th>
                                <th>Quantity</th>
                                <th>Amount (Tsh)</th>
                            </tr>
                        </thead>
                        <tbody>
                            {invoiceData.services.map((service, index) => (
                                <tr key={index}>
                                    <td>{service.name}</td>
                                    <td>{service.price}</td>
                                    <td>{service.quantity}</td>
                                    <td>{service.price * service.quantity}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>

                    <div className="invoice-total mt-4">
                        <p><strong>Sub Total:</strong> {invoiceData.subTotal} {invoiceData.currency}</p>
                        <p><strong>Discount:</strong> {invoiceData.discount} {invoiceData.currency}</p>
                        <p><strong>Tax:</strong> {invoiceData.tax} {invoiceData.currency}</p>
                        <p><strong>Grand Total:</strong> {invoiceData.grandTotal} {invoiceData.currency}</p>
                    </div>

                    <button className="btn btn-success mt-4" onClick={generatePDF}>
                        Generate PDF
                    </button>
                </div>
            </div>
        </div>
    );
};

export default Invoice;
