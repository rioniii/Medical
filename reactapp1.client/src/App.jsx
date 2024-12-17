import React, { Component, Suspense } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import Footer from "./Footer";
import Card from "./Card";
import IntervalSlider from './IntervalSlider';
import Ballina from './Ballina';
import Dashboard from './Dashboard/Components/Dashboard';
import RepartCrud from './Dashboard/Components/RepartCrud';
import Pacientet from './Dashboard/Components/Pacientet';
import { Route, Routes, HashRouter, Navigate } from 'react-router-dom';
import i4 from './assets/i4.jpg';
import i2 from './assets/i2.jpg';
import i3 from './assets/i3.jpg';
import Sidebar from "./Dashboard/Components/Sidebar";
import Header from './Header';
import DashboardStats from './Dashboard/Components/DashboardStats';
import Appointments from './Dashboard/Components/Appointments';
import Payments from './Dashboard/Components/Payments';
import Services from './Dashboard/Components/Services';
import Records from './Dashboard/Components/Records';
import Room from './Dashboard/Components/Room';
import Invoice from './Dashboard/Components/Invoice';
import DashboardAdmin from './Dashboard/Components/DashboardAdmin';
import ManageUsers from './Dashboard/Components/ManageUsers';
import Announcements from './Dashboard/Components/Announcements';
import PaymentAdmin from './Dashboard/Components/PaymentAdmin';

// Lazy loading components
const RegisterForm = React.lazy(() => import("./RegisterForm"));
const LoginForm = React.lazy(() => import("./LoginForm"));
const AboutUs = React.lazy(() => import("./AboutUs"));
const Contact = React.lazy(() => import("./Contact"));

// Error Boundary Component
class ErrorBoundary extends Component {
    constructor(props) {
        super(props);
        this.state = { hasError: false };
    }

    static getDerivedStateFromError(error) {
        return { hasError: true };
    }

    componentDidCatch(error, errorInfo) {
        console.error("Error occurred in child component:", error, errorInfo);
    }

    render() {
        if (this.state.hasError) {
            return <h1>Something went wrong.</h1>;
        }
        return this.props.children;
    }
}

// ProtectedRoute Component
const ProtectedRoute = ({ children, roles }) => {
    const token = localStorage.getItem('token');
    const userRoles = JSON.parse(localStorage.getItem('userRoles')) || [];

    // If no token, redirect to login page
    if (!token) {
        return <Navigate to="/LoginForm" />;
    }

    // If user does not have the required roles, redirect to home page
    if (roles && !roles.some(role => userRoles.includes(role))) {
        return <Navigate to="/" />;
    }

    return children; // If authenticated, render the children
};

class App extends Component {
    render() {
        return (
            <Suspense fallback={<div className="text-center mt-5"><div className="spinner-border text-primary"></div></div>}>
                <HashRouter>
                    <Header />
                    <Routes>
                        <Route path="/" element={
                            <div>
                                <Ballina />
                                {/*<Card />*/}

                            </div>
                        } />
                        <Route path="/RegisterForm" element={<ErrorBoundary><RegisterForm /></ErrorBoundary>} />
                        <Route path="/LoginForm" element={<ErrorBoundary><LoginForm /></ErrorBoundary>} />
                        <Route path="/AboutUs" element={<ErrorBoundary><AboutUs /></ErrorBoundary>} />
                        <Route path="/Contact" element={<ErrorBoundary><Contact /></ErrorBoundary>} />


                        {/* Protected Routes */}
                        <Route path="/Dashboard" element={
                            <ProtectedRoute roles={['Doctor']}>
                                <ProtectedRoute>
                                    <ErrorBoundary><Dashboard /></ErrorBoundary>
                                </ProtectedRoute>
                            </ProtectedRoute>
                        } />
                        <Route path="/Pacientet" element={
                            <ProtectedRoute roles={['Doctor']}>
                                <ProtectedRoute>
                                    <ErrorBoundary><Pacientet /></ErrorBoundary>
                                </ProtectedRoute>
                            </ProtectedRoute>

                        } /><Route path="/Pacientet" element={
                            <ProtectedRoute roles={['Doctor']}>
                                <ProtectedRoute>
                                    <ErrorBoundary><DashboardStats /></ErrorBoundary>
                                </ProtectedRoute>
                            </ProtectedRoute>
                        } />

                        <Route path="/Appointments" element={
                            <ProtectedRoute roles={['Doctor']}>
                                <ProtectedRoute>
                                    <ErrorBoundary><Appointments /></ErrorBoundary>
                                </ProtectedRoute>
                            </ProtectedRoute>

                        } />
                        <Route path="/Payments" element={
                            <ProtectedRoute roles={['Doctor']}>
                                <ProtectedRoute>
                                    <ErrorBoundary><Payments /></ErrorBoundary>
                                </ProtectedRoute>
                            </ProtectedRoute>

                        } /> <Route path="/Services" element={
                            <ProtectedRoute roles={['Doctor']}>
                                <ProtectedRoute>
                                    <ErrorBoundary><Services /></ErrorBoundary>
                                </ProtectedRoute>
                            </ProtectedRoute>

                        } />
                        <Route path="/Records" element={
                            <ProtectedRoute roles={['Doctor']}>
                                <ProtectedRoute>
                                    <ErrorBoundary><Records /></ErrorBoundary>
                                </ProtectedRoute>
                            </ProtectedRoute>

                        } />
                        <Route path="/Room" element={
                            <ProtectedRoute roles={['Doctor']}>
                                <ProtectedRoute>
                                    <ErrorBoundary><Room /></ErrorBoundary>
                                </ProtectedRoute>
                            </ProtectedRoute>

                        } />
                        <Route path="/Invoice" element={
                            <ProtectedRoute roles={['Doctor']}>
                                <ProtectedRoute>
                                    <ErrorBoundary><Invoice /></ErrorBoundary>
                                </ProtectedRoute>
                            </ProtectedRoute>

                        } /> 

                        <Route path="/AdminDashboard" element={
                            <ProtectedRoute roles={['Administrator']}>
                                <ErrorBoundary><DashboardAdmin /></ErrorBoundary>
                            </ProtectedRoute>
                        } />

                        <Route path="/ManageUsers" element={
                            <ProtectedRoute roles={['Administrator']}>
                                <ErrorBoundary><ManageUsers /></ErrorBoundary>
                            </ProtectedRoute>
                        } />

                        <Route path="/Announcements" element={
                            <ProtectedRoute roles={['Administrator']}>
                                <ErrorBoundary><Announcements /></ErrorBoundary>
                            </ProtectedRoute>
                        } />
                        <Route path="/PaymentAdmin" element={
                            <ProtectedRoute roles={['Administrator']}>
                                <ErrorBoundary><PaymentAdmin /></ErrorBoundary>
                            </ProtectedRoute>
                        } />


                    </Routes>
                    <Footer />
                </HashRouter>
            </Suspense>
        );
    }
}

export default App;
