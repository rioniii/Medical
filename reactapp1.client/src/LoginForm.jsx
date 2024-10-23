import React, { useState } from 'react';
import { MDBBtn, MDBCard, MDBCardBody, MDBInput } from 'mdb-react-ui-kit';
import { NavLink, useNavigate } from 'react-router-dom';
import contactImage from './assets/R.jpeg';

const LoginForm = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const navigate = useNavigate();

    const handleRegister = async (event) => {
        event.preventDefault();

        const userData = { email, password };

        try {
            const response = await fetch('https://localhost:7107/api/Auth/Login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(userData),
            });

            if (!response.ok) {
                setErrorMessage('Login failed. Please check your email and password.');
                return;
            }

            const data = await response.json();
            localStorage.setItem('token', data.token);
            localStorage.setItem('userRoles', JSON.stringify(data.roles || []));
            navigate(data.roles.includes('Doctor') ? '/' : '/');
        } catch (error) {
            setErrorMessage('An error occurred. Please try again later.');
        }
    };

    return (
        <div className="register-form-container" style={{
            backgroundImage: `url(${contactImage})`,
            backgroundColor: 'rgba(255, 255, 255, 0.5)',
            backgroundSize: 'cover',
            backgroundPosition: 'center',
            minHeight: '100vh',
            display: 'flex',
            flexDirection: 'column'
        }}>
            <div style={{
                flex: 1,
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                padding: '10px'
            }}>
                <MDBCard className='p-3' style={{
                    maxWidth: '400px',
                    width: '100%',
                    backgroundColor: 'rgba(255, 255, 255, 0.9)',
                    borderRadius: '10px',
                    boxShadow: '0px 0px 5px rgba(0, 0, 0, 0.1)'
                }}>
                    <MDBCardBody className='px-5 py-4'>
                        <h2 className="text-uppercase text-center mb-3">Log In</h2>
                        <form onSubmit={handleRegister}>
                            <MDBInput
                                placeholder='Your Email'
                                type='email'
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                style={{ marginBottom: '1rem' }}
                            />
                            <MDBInput
                                placeholder='Password'
                                type='password'
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                            />
                            {errorMessage && <div className="text-danger mb-2">{errorMessage}</div>}
                            <MDBBtn
                                className='register-btn'
                                size='lg'
                                type='submit'
                                style={{
                                    width: '120px',
                                    height: '40px',
                                    alignSelf: 'center',
                                    fontSize: '0.9rem',
                                }}
                            >Login</MDBBtn>
                        </form>
                        <NavLink to="/ForgotPassword" className="text-center d-block mt-2" style={{ color: 'green', fontSize: '0.9rem' }}>
                            Forgot Password?
                        </NavLink>
                    </MDBCardBody>
                </MDBCard>
            </div>
        </div>
    );
};

export default LoginForm;
