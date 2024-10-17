import React, { Component, Suspense } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import Footer from "./Footer";
import Card from "./Card";
import IntervalSlider from './IntervalSlider';
import Ballina from './Ballina';
import PatientCRUD from './Dashboard/Components/PatientCRUD';
import RepartCrud from './Dashboard/Components/RepartCrud';
import { Route, Routes, HashRouter } from 'react-router-dom';
import i4 from './assets/i4.jpg';
import i2 from './assets/i2.jpg';
import i3 from './assets/i3.jpg';
import MjekuCRUD from './Dashboard/Components/MjekuCRUD';
import Header from './Header'; // Import the Header component

const RegisterForm = React.lazy(() => import("./RegisterForm"));
const LoginForm = React.lazy(() => import("./LoginForm"));
const AboutUs = React.lazy(() => import("./AboutUs"));
const Contact = React.lazy(() => import("./Contact"));

class App extends Component {
    render() {
        return (
            <Suspense fallback={<div>Loading...</div>}>
                <HashRouter>
                    <Routes>
                        <Route path="/" element={
                            <div>
                                <Header />  {/* Show the Header component here */}
                                <Ballina />
                                <IntervalSlider images={[i4, i3, i2]} interval={3000} />
                                <Card />
                                <Footer />
                            </div>
                        } />
                        <Route path="/RegisterForm" element={<RegisterForm />}></Route>
                        <Route path="/LoginForm" element={<LoginForm />}></Route>
                        <Route path="/AboutUs" element={<AboutUs />}></Route>
                        <Route path="/Contact" element={<Contact />}></Route>
                        <Route path="/PatientCRUD" element={<PatientCRUD />}></Route>
                        <Route path="/RepartCrud" element={<RepartCrud />}></Route>
                        <Route path="/MjekuCRUD" element={<MjekuCRUD />}></Route>
                    </Routes>
                </HashRouter>
            </Suspense>
        );
    }
}

export default App;
