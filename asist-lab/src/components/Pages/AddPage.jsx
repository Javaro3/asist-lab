import React, { useState } from 'react';
import Header from '../UI/Header';
import { useNavigate } from 'react-router-dom';
import { MapContainer, TileLayer, Marker, Polyline, useMapEvents } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import L from 'leaflet';

import markerIcon from 'leaflet/dist/images/marker-icon.png';
import markerIconRetina from 'leaflet/dist/images/marker-icon-2x.png';
import markerShadow from 'leaflet/dist/images/marker-shadow.png';
import ErrorMessage from '../UI/ErrorMessage';

const DefaultIcon = L.icon({
  iconUrl: markerIcon,
  iconRetinaUrl: markerIconRetina,
  shadowUrl: markerShadow,
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  shadowSize: [41, 41]
});

L.Marker.prototype.options.icon = DefaultIcon;

const AddPage = ({ setIsAuthenticated }) => {
  const [title, setTitle] = useState('');
  const [startDate, setStartDate] = useState('');
  const [endDate, setEndDate] = useState('');
  const [description, setDescription] = useState('');
  const [images, setImages] = useState([]);
  const [routePoints, setRoutePoints] = useState([]);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleImageUpload = (e) => {
    const files = Array.from(e.target.files);
    setImages(files);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const formattedRoutePoints = routePoints.map((point, index) => ({
      latitude: point.lat,
      longitude: point.lng,
      order: index + 1,
    }));

    const formData = new FormData();
    formData.append('name', title);
    formData.append('expectedStartTime', startDate);
    formData.append('expectedFinishTime', endDate);
    formData.append('description', description);
    formattedRoutePoints.forEach((point, index) => {
      formData.append(`points[${index}].latitude`, point.latitude);
      formData.append(`points[${index}].longitude`, point.longitude);
      formData.append(`points[${index}].order`, point.order);
    });
    images.forEach((image) => {
      formData.append('images', image);
    });

    const response = await fetch('https://localhost:7213/Trip/put', {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('jwtToken')}`
      },
      body: formData
    });

    if (response.ok) {
      navigate('/main');
    } else {
      const errorData = await response.json();
      setError(errorData.message);
    }
  };

  const MapClickHandler = () => {
    useMapEvents({
      click(e) {
        setRoutePoints((prevPoints) => [
          ...prevPoints,
          { lat: e.latlng.lat, lng: e.latlng.lng },
        ]);
      },
    });
    return null;
  };

  const handleMarkerRightClick = (index) => {
    setRoutePoints((prevPoints) =>
      prevPoints.filter((_, i) => i !== index)
    );
  };

  return (
    <div>
      <Header setIsAuthenticated={setIsAuthenticated} />
      <div className="flex flex-col md:flex-row p-8 max-w-5xl mx-auto">
        <form onSubmit={handleSubmit} className="md:w-1/3 md:mr-4">
          <h1 className="text-3xl mb-6">Add a New Trip</h1>
          <ErrorMessage message={error} />
          <label className="block mb-2">
            Trip Title:
            <input
              type="text"
              value={title}
              onChange={(e) => setTitle(e.target.value)}
              required
              className="w-full p-2 border border-gray-300 rounded"
            />
          </label>
          <label className="block mb-2">
            Expected Start Time:
            <input
              type="datetime-local"
              value={startDate}
              onChange={(e) => setStartDate(e.target.value)}
              required
              className="w-full p-2 border border-gray-300 rounded"
            />
          </label>
          <label className="block mb-2">
            Expected Finish Time:
            <input
              type="datetime-local"
              value={endDate}
              onChange={(e) => setEndDate(e.target.value)}
              required
              className="w-full p-2 border border-gray-300 rounded"
            />
          </label>
          <label className="block mb-2">
            Description:
            <textarea
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              className="w-full p-2 border border-gray-300 rounded"
            />
          </label>
          <label className="block mb-2">
            Upload Images:
            <input
              type="file"
              multiple
              accept="image/*"
              onChange={handleImageUpload}
              className="w-full p-2 border border-gray-300 rounded"
            />
          </label>
          <button
            type="submit"
            className="mt-4 bg-blue-500 text-white py-2 px-4 rounded hover:bg-blue-600"
          >
            Add Trip
          </button>
        </form>
        <div className="mt-6 md:mt-0 md:w-2/3">
          <h2 className="text-lg mb-2">Select Route:</h2>
          <MapContainer
            center={[52.4, 31]}
            zoom={10}
            style={{ width: '100%', height: '500px' }}
            doubleClickZoom={false}
          >
            <TileLayer
              url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            <MapClickHandler />
            {routePoints.map((point, index) => (
              <Marker
                key={index}
                position={[point.lat, point.lng]}
                eventHandlers={{
                  contextmenu: () => handleMarkerRightClick(index),
                }}
              />
            ))}
            {routePoints.length > 1 && (
              <Polyline positions={routePoints.map(point => [point.lat, point.lng])} color="blue" />
            )}
          </MapContainer>
        </div>
      </div>
    </div>
  );
};

export default AddPage;
