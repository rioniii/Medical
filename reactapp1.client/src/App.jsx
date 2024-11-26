import React, { Component, Suspense } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import Footer from "./Footer";
import Card from "./Card";
import IntervalSlider from './IntervalSlider';
import Ballina from './Ballina';
import Dashboard from './Dashboard/Components/Dashboard';
import RepartCrud from './Dashboard/Components/RepartCrud';
import MjekuCRUD from './Dashboard/Components/MjekuCRUD';
import Pacientet from './Dashboard/Components/Pacientet';
import { Route, Routes, HashRouter, Navigate } from 'react-router-dom';
import i4 from './assets/i4.jpg';
import i2 from './assets/i2.jpg';
import i3 from './assets/i3.jpg';
import Sidebar from "./Dashboard/Components/Sidebar";
import Header from './Header';

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
                                <IntervalSlider images={[i4, i3, i2]} interval={3000} />
                                <Card />
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
                        } />
                        
                        <Route path="/RepartCrud" element={
                            <ProtectedRoute roles={['Administrator']}>
                                <ErrorBoundary><RepartCrud /></ErrorBoundary>
                            </ProtectedRoute>
                        } />
                       
                        <Route path="/MjekuCRUD" element={
                            <ProtectedRoute roles={['Doctor']}>
                                <ErrorBoundary><MjekuCRUD /></ErrorBoundary>
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
