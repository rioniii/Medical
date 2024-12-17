import React, { useEffect, useState } from "react";
import axios from "axios";
import Sidebar from "./Sidebar"; // Assuming Sidebar is a separate component
import "./PaymentAdmin.css"; // CSS for styling

const PaymentAdmin = () => {
    const [payments, setPayments] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchPayments();
    }, []);

    const fetchPayments = async () => {
        try {
            setLoading(true);
            const response = await axios.get("https://localhost:7107/api/Fatura");
            if (Array.isArray(response.data)) {
                setPayments(response.data);
            } else {
                throw new Error("Invalid data format: payments should be an array");
            }
            setError(null);
        } catch (err) {
            setError("Failed to fetch payments. Please try again.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="d-flex" style={{ backgroundColor: "#f5f6f7" }}>
            {/* Sidebar for admin */}
            <Sidebar userType="admin" />

            <div className="admin-dashboard">
                {error && <div className="error">{error}</div>}
                {loading ? (
                    <div className="loading">Loading...</div>
                ) : (
                    <div className="payment-list">
                        <h2>Payments</h2>
                        <table className="payment-table">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Patient</th>
                                    <th>Date</th>
                                    <th>Status</th>
                                    <th>Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                                {payments.length > 0 ? (
                                    payments.map((payment) => (
                                        <tr key={payment.id}>
                                            <td>{payment.id}</td>
                                            <td>{payment.pacientId}</td>
                                            <td>{new Date(payment.data).toLocaleString()}</td>
                                            <td>{payment.paguar ? "Paid" : "Pending"}</td>
                                            <td>{payment.shuma}</td>
                                        </tr>
                                    ))
                                ) : (
                                    <tr>
                                        <td colSpan="5">No payments found.</td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </div>
    );
};

export default PaymentAdmin;
