/*import React from "react";
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
    FaBed,
    FaHospital,
    FaClinicMedical 
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
                    <Link to="/Appointments" className="sidebar-link">
                        <FaCalendarAlt className="sidebar-icon" /> Appointments
                    </Link>
                </li>
                <li>
                <li>
                    <Link to="/Records" className="sidebar-link">
                        <FaFileAlt className="sidebar-icon" /> Medical Record
                    </Link>
                </li>
                    <li>
                        <Link to="/Room" className="sidebar-link">
                            <FaBed className="sidebar-icon" /> Room 
                        </Link>
                    </li>
                    <Link to="/Payments" className="sidebar-link">
                        <FaMoneyCheckAlt className="sidebar-icon" /> Payments
                    </Link>
                </li>
                
                <li>
                    <Link to="/Services" className="sidebar-link">
                        <FaClinicMedical className="sidebar-icon" /> Services
                    </Link>
                </li>
                <li>
                    <Link to="/Invoice" className="sidebar-link">
                        <FaCog className="sidebar-icon" /> Invoice
                    </Link>
                </li>

            </ul>
        </div>
    );
};

export default Sidebar;
*/

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
    FaBed,
    FaHospital,
    FaClinicMedical,
} from "react-icons/fa";
import { Link } from "react-router-dom";
import "./Sidebar.css"; // Import the CSS file

const Sidebar = ({ userType }) => {
    // Define sidebar items based on userType
    const sidebarItems = userType === "admin"
        ? [
            { path: "/AdminDashboard", icon: <FaHome />, label: "Dashboard" },
            { path: "/ManageUsers", icon: <FaUsers />, label: "Manage Users" },
            { path: "/Announcements", icon: <FaBullhorn />, label: "Announcements" },
            { path: "/PaymentAdmin", icon: <FaMoneyCheckAlt />, label: "Payments" },
            { path: "/Services", icon: <FaClinicMedical />, label: "Services" },
            { path: "/Settings", icon: <FaCog />, label: "Settings" },
        ]
        : [
            { path: "/Dashboard", icon: <FaHome />, label: "Dashboard" },
            { path: "/Pacientet", icon: <FaUserAlt />, label: "Patients" },
            { path: "/Appointments", icon: <FaCalendarAlt />, label: "Appointments" },
            { path: "/Records", icon: <FaFileAlt />, label: "Medical Records" },
            { path: "/Room", icon: <FaBed />, label: "Room" },
            { path: "/Payments", icon: <FaMoneyCheckAlt />, label: "Payments" },
            { path: "/Services", icon: <FaClinicMedical />, label: "Services" },
            { path: "/Invoice", icon: <FaFileInvoiceDollar />, label: "Invoice" },
        ];

    return (
        <div className="sidebar-container">
            <h5 className="sidebar-title">{userType === "admin" ? "Admin Dashboard" : "Doctor Dashboard"}</h5>
            <ul className="sidebar-list">
                {sidebarItems.map((item, index) => (
                    <li key={index}>
                        <Link to={item.path} className="sidebar-link">
                            {item.icon} {item.label}
                        </Link>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Sidebar;
