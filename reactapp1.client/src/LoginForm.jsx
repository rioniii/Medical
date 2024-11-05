import React, { useState, useEffect } from 'react';
import { MDBBtn, MDBCard, MDBCardBody, MDBInput } from 'mdb-react-ui-kit';
import { NavLink, useNavigate } from 'react-router-dom';
import contactImage from './assets/R.jpeg';

const LoginForm = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const navigate = useNavigate();

    const REFRESH_INTERVAL = 100 * 60 * 1000; // 100 minutes in milliseconds (refreshing 1 minute before expiration)
    let refreshTimerId = null;

    const validateForm = () => {
        if (!email || !password) {
            setErrorMessage('Please fill in all fields.');
            return false;
        }
        if (!/\S+@\S+\.\S+/.test(email)) {
            setErrorMessage('Please enter a valid email address.');
            return false;
        }
        return true;
    };

    const handleLogin = async (event) => {
        event.preventDefault();
        if (!validateForm()) return;

        setIsLoading(true);
        setErrorMessage('');

        try {
            const response = await fetch('https://localhost:7107/api/Auth/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    email: email,
                    password: password
                })
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Login failed');
            }

            const userData = await response.json();
            
            // Store the tokens and user data
            localStorage.setItem('accessToken', userData.token);
            localStorage.setItem('refreshToken', userData.refreshToken);
            localStorage.setItem('tokenExpiration', userData.expiration);
            localStorage.setItem('userRoles', JSON.stringify(userData.roles));

            startRefreshTimer();
            
            // Navigate based on role
            if (userData.roles.includes('Doctor')) {
                navigate('/doctor-dashboard');
            } else {
                navigate('/');
            }

        } catch (error) {
            setErrorMessage(error.message || 'An error occurred during login.');
        } finally {
            setIsLoading(false);
        }
    };

    const startRefreshTimer = () => {
        // Clear any existing timer
        if (refreshTimerId) {
            clearInterval(refreshTimerId);
        }

        // Set up new refresh interval
        refreshTimerId = setInterval(refreshToken, REFRESH_INTERVAL);
    };

    const refreshToken = async () => {
        try {
            const storedRefreshToken = localStorage.getItem('refreshToken');
            if (!storedRefreshToken) {
                throw new Error('No refresh token available');
            }

            const response = await fetch('/api/auth/refreshtoken', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    refreshToken: storedRefreshToken
                })
            });

            if (!response.ok) {
                throw new Error('Token refresh failed');
            }

            const data = await response.json();
            
            // Update stored tokens
            localStorage.setItem('accessToken', data.token);
            localStorage.setItem('refreshToken', data.refreshToken);
            localStorage.setItem('tokenExpiration', data.expiration);

        } catch (error) {
            console.error('Error refreshing token:', error);
            // Clear stored tokens and redirect to login
            localStorage.clear();
            if (refreshTimerId) {
                clearInterval(refreshTimerId);
            }
            navigate('/login');
        }
    };

    // Check for existing token on component mount
    useEffect(() => {
        const token = localStorage.getItem('accessToken');
        if (token) {
            startRefreshTimer();
        }

        // Cleanup on component unmount
        return () => {
            if (refreshTimerId) {
                clearInterval(refreshTimerId);
            }
        };
    }, []);

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
                padding: '10px',
                marginTop: '50px' // Adjust this value to move the login form lower
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
                        <form onSubmit={handleLogin}>
                            <MDBInput
                                placeholder='Your Email'
                                type='email'
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                style={{ marginBottom: '1rem' }}
                                required
                            />
                            <MDBInput
                                placeholder='Password'
                                type='password'
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                required
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
                                    marginTop: '1rem', // Add margin to move the button down
                                }}
                                disabled={isLoading}
                            >
                                {isLoading ? 'Logging in...' : 'Login'}
                            </MDBBtn>
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