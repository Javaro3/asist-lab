import React, { useEffect, useState } from 'react';
import Header from '../UI/Header';
import { useNavigate } from 'react-router-dom';
import TripMyCard from '../UI/TripMyCard';

const MainPage = ({ setIsAuthenticated }) => {
  const [trips, setTrips] = useState([]);
  const navigate = useNavigate();


  const fetchTrips = async () => {
    const response = await fetch('https://localhost:7213/Trip/get-my', {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('jwtToken')}`,
      },
    });

    if (response.ok) {
      const data = await response.json();
      setTrips(data);
    } else {
      setIsAuthenticated(false);
      navigate('/login');
    }
  };

  useEffect(() => {
    fetchTrips();
  }, [setIsAuthenticated, navigate]);

  return (
    <div>
      <Header setIsAuthenticated={setIsAuthenticated} />
      <div className="p-8 max-w-4xl mx-auto">
        <h1 className="text-3xl mb-6">My Trips</h1>
        {trips.map(trip => (
          <TripMyCard key={trip.id} setIsAuthenticated={setIsAuthenticated} trip={trip} fetchTrips={fetchTrips} />
        ))}
      </div>
    </div>
  );
};

export default MainPage;
