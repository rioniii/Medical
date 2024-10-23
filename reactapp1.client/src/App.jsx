import React, { Component, Suspense } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import Footer from "./Footer";
import Card from "./Card";
import IntervalSlider from './IntervalSlider';
import Ballina from './Ballina';
import PatientCRUD from './Dashboard/Components/PatientCRUD';
import RepartCrud from './Dashboard/Components/RepartCrud';
import MjekuCRUD from './Dashboard/Components/MjekuCRUD';
import { Route, Routes, HashRouter } from 'react-router-dom';
import i4 from './assets/i4.jpg';
import i2 from './assets/i2.jpg';
import i3 from './assets/i3.jpg';
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

class App extends Component {
    render() {
        return (
            <Suspense fallback={<div>Loading...</div>}>
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
                        <Route path="/PatientCRUD" element={<ErrorBoundary><PatientCRUD /></ErrorBoundary>} />
                        <Route path="/RepartCrud" element={<ErrorBoundary><RepartCrud /></ErrorBoundary>} />
                        <Route path="/MjekuCRUD" element={<ErrorBoundary><MjekuCRUD /></ErrorBoundary>} />
                    </Routes>
                    <Footer />
                </HashRouter>
            </Suspense>
        );
    }
}

export default App;
