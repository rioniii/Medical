import React from "react";
import {
    FaHome,
    FaUserAlt,
    FaCalendarAlt,
    FaFileInvoiceDollar,
    FaMedkit,
    FaCog,
    FaFileAlt,
    FaMoneyCheckAlt,
    FaUsers,
    FaBullhorn,
} from "react-icons/fa";
import { Link } from "react-router-dom";
import "./Sidebar.css"; // Import the CSS file


const Sidebar = () => {
    return (
        <div className="sidebar-container">
            <h5 className="sidebar-title">Hospital Dashboard</h5>
            <ul className="sidebar-list">
                <li>
                    <Link to="/Dashboard" className="sidebar-link">
                        <FaHome className="sidebar-icon" /> Dashboard
                    </Link>
                </li>
                <li>
                    <Link to="/Pacientet" className="sidebar-link">
                        <FaUserAlt className="sidebar-icon" /> Patients
                    </Link>
                </li>
                <li>
                    <Link to="#" className="sidebar-link">
                        <FaMedkit className="sidebar-icon" /> Doctors
                    </Link>
                </li>
                <li>
                    <Link to="/Appointments" className="sidebar-link">
                        <FaCalendarAlt className="sidebar-icon" /> Appointments
                    </Link>
                </li>
                <li>
                    <Link to="#" className="sidebar-link">
                        <FaMoneyCheckAlt className="sidebar-icon" /> Payments
                    </Link>
                </li>
                <li>
                    <Link to="#" className="sidebar-link">
                        <FaFileInvoiceDollar className="sidebar-icon" /> Invoices
                    </Link>
                </li>
                <li>
                    <Link to="#" className="sidebar-link">
                        <FaFileAlt className="sidebar-icon" /> Services
                    </Link>
                </li>
                <li>
                    <Link to="#" className="sidebar-link">
                        <FaBullhorn className="sidebar-icon" /> Campaigns
                    </Link>
                </li>
                <li>
                    <Link to="#" className="sidebar-link">
                        <FaCog className="sidebar-icon" /> Settings
                    </Link>
                </li>
                <li>
                    <Link to="#" className="sidebar-link">
                        <FaUsers className="sidebar-icon" /> Receptions
                    </Link>
                </li>
            </ul>
        </div>
    );
};

export default Sidebar;
