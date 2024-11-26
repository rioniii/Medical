/*import React, { useState, useEffect } from "react";
import Navigation from "./Navigation.jsx";
import Sidebar from "./Sidebar.jsx";
import './Pacientet.jsx';


const Dashboard = ({ children }) => {
    return (
        <div className="layout">
            <Navigation />
            <div className="layout-content">
                <Sidebar />
                <main className="main-content">{children}</main>
            </div>
        </div>
    );
};

export default Dashboard;
*/

import React from 'react';
import Sidebar from './Sidebar';
import Widget from './Widget';

const Dashboard = () => {
    return (
        <div className="d-flex">
            <Sidebar />
            
        </div>
    );
};

export default Dashboard;
