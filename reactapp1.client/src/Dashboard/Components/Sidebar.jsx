import React from "react";
import { FaHome, FaUserAlt, FaCalendarAlt, FaFileInvoiceDollar, FaMedkit, FaCog, FaFileAlt, FaMoneyCheckAlt, FaUsers, FaBullhorn } from "react-icons/fa";
import { Link } from 'react-router-dom';
import './Sidebar.css'; // Import the CSS file

const Sidebar = () => {
    return (
        <div style={{ backgroundColor: "#f8f9fa", height: "100vh", padding: "20px", width: "240px" }}>
            <h5 style={{ color: "#2d3955", fontWeight: "bold", marginBottom: "20px" }}>Hospital Dashboard</h5>
            <ul style={{ listStyle: "none", padding: 0 }}>
                <li className="mb-3">
                    <Link to="/Dashboard" className="sidebar-link">
                        <FaHome style={iconStyle} /> Dashboard
                    </Link>
                </li>
                <li className="mb-3">
                    <Link to="/Pacientet" className="sidebar-link">
                        <FaUserAlt style={iconStyle} /> Patients
                    </Link>
                </li>
                
                <li className="mb-3">
                    <Link to="#" className="sidebar-link">
                        <FaMedkit style={iconStyle} /> Doctors
                    </Link>
                </li>
                <li className="mb-3">
                    <Link to="#" className="sidebar-link">
                        <FaCalendarAlt style={iconStyle} /> Appointments
                    </Link>
                </li>
                <li className="mb-3">
                    <Link to="#" className="sidebar-link">
                        <FaMoneyCheckAlt style={iconStyle} /> Payments
                    </Link>
                </li>
                <li className="mb-3">
                    <Link to="#" className="sidebar-link">
                        <FaFileInvoiceDollar style={iconStyle} /> Invoices
                    </Link>
                </li>
                <li className="mb-3">
                    <Link to="#" className="sidebar-link">
                        <FaFileAlt style={iconStyle} /> Services
                    </Link>
                </li>
                <li className="mb-3">
                    <Link to="#" className="sidebar-link">
                        <FaBullhorn style={iconStyle} /> Campaigns
                    </Link>
                </li>
                <li className="mb-3">
                    <Link to="#" className="sidebar-link">
                        <FaCog style={iconStyle} /> Settings
                    </Link>
                </li>
                <li className="mb-3">
                    <Link to="#" className="sidebar-link">
                        <FaUsers style={iconStyle} /> Receptions
                    </Link>
                </li>
            </ul>
        </div>
    );
};

// Icon style
const iconStyle = {
    marginRight: "10px",
    color: "#6db7ab",
    fontSize: "18px",
};

export default Sidebar;
