import React, { Component, Suspense } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import Footer from "./Footer";
import Card from "./Card";
import IntervalSlider from './IntervalSlider';
import Ballina from './Ballina';
import PatientCRUD from './Dashboard/Components/PatientCRUD';
import RepartCrud from './Dashboard/Components/RepartCrud';
import { Route, Routes, HashRouter, Navigate } from 'react-router-dom';
import i4 from './assets/i4.jpg';
import i2 from './assets/i2.jpg';
import i3 from './assets/i3.jpg';
import MjekuCRUD from './Dashboard/Components/MjekuCRUD';
import Header from './Header';

const RegisterForm = React.lazy(() => import("./RegisterForm"));
const LoginForm = React.lazy(() => import("./LoginForm"));
const AboutUs = React.lazy(() => import("./AboutUs"));
const Contact = React.lazy(() => import("./Contact"));

class App extends Component {
    render() {
        const token = localStorage.getItem('token');
        let userRole = 'User'; // Default to 'User'

        if (token) {
            try {
                const payload = JSON.parse(atob(token.split('.')[1]));
                console.log("Decoded Payload:", payload); // Log the decoded payload
                userRole = payload.role; // Ensure this matches how your role is structured
            } catch (error) {
                console.error("Token decoding error:", error); // Log any decoding errors
            }
        }

        return (
            <Suspense fallback={<div>Loading...</div>}>
                <HashRouter>
                    <Header userRole={userRole} />

                    <Routes>
                        <Route path="/" element={
                            <div>
                                <Ballina />
                                <IntervalSlider images={[i4, i3, i2]} interval={3000} />
                                <Card />
                            </div>
                        } />
                        <Route path="/RegisterForm" element={<RegisterForm />} />
                        <Route path="/LoginForm" element={<LoginForm />} />
                        <Route path="/AboutUs" element={<AboutUs />} />
                        <Route path="/Contact" element={<Contact />} />
                        <Route path="/PatientCRUD" element={<PatientCRUD />} />
                        <Route path="/RepartCrud" element={<RepartCrud />} />
                        <Route path="/MjekuCRUD" element={<MjekuCRUD />} />
                        <Route path="/admin-dashboard" element={userRole === 'Admin' ? <MjekuCRUD /> : <Navigate to="/" />} />
                        <Route path="*" element={<Navigate to="/" />} />
                    </Routes>

                    <Footer />
                </HashRouter>
            </Suspense>
        );
    }
}

export default App;
