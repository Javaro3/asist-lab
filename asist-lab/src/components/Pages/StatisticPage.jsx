import React, { useState, useEffect } from 'react';
import Header from '../UI/Header';

const StatisticPage = ({ setIsAuthenticated }) => {
  const [trips, setTrips] = useState([]);
  const [yearFilter, setYearFilter] = useState('');
  const [filteredTrips, setFilteredTrips] = useState([]);

  useEffect(() => {
    const fetchTrips = async () => {
      const response = await fetch('https://localhost:7213/Trip/get-history', {
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

    fetchTrips();
  }, []);

  useEffect(() => {
    setFilteredTrips(
      trips.filter(trip => {
        const tripYear = new Date(trip.expectedStartTime).getFullYear();
        return (
          (yearFilter ? tripYear === parseInt(yearFilter) : true)
        );
      })
    );
  }, [yearFilter, trips]);

  const years = [...new Set(trips.map(trip => new Date(trip.expectedStartTime).getFullYear()))];

  const parseDuration = (duration) => {
    const [hours, minutes, seconds] = duration.split(':');
    return (
      parseInt(hours, 10) * 3600 +
      parseInt(minutes, 10) * 60 +
      parseFloat(seconds)
    );
  };

  const totalTrips = filteredTrips.length;
  const totalDurationSeconds = filteredTrips.reduce((acc, trip) => acc + parseDuration(trip.duration), 0);
  const averageDurationSeconds = filteredTrips.length > 0 ? totalDurationSeconds / filteredTrips.length : 0;

  return (
    <div>
       <Header setIsAuthenticated={setIsAuthenticated} />
      <div className="p-8 max-w-5xl mx-auto">
        <h1 className="text-3xl mb-6">Trip Statistics</h1>

        <div className="flex space-x-4 mb-6">
          <div>
            <label className="block mb-2">Filter by Year</label>
            <select
              value={yearFilter}
              onChange={(e) => setYearFilter(e.target.value)}
              className="p-2 border border-gray-300 rounded"
            >
              <option value="">All Years</option>
              {years.map(year => (
                <option key={year} value={year}>{year}</option>
              ))}
            </select>
          </div>
        </div>

        <div className="mb-6">
          <h2 className="text-xl font-semibold">Summary</h2>
          <p>Total Trips: {totalTrips}</p>
          <p>Total Duration: {totalDurationSeconds}</p>
          <p>Average Duration: {averageDurationSeconds.toFixed(2)}</p>
        </div>

        <div className="grid grid-cols-1 gap-4">
          {filteredTrips.map((trip, index) => (
            <div key={index} className="border rounded-lg p-4 shadow">
              <h3 className="text-lg font-semibold">{trip.name}</h3>
              <p>Start Date: {new Date(trip.expectedStartTime).toLocaleString()}</p>
              <p>End Date: {new Date(trip.expectedFinishTime).toLocaleString()}</p>
              <p>Duration: {trip.duration}</p>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default StatisticPage;