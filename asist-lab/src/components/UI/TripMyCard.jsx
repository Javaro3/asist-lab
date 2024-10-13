import React, { useState } from 'react';
import { MapContainer, TileLayer, Marker, Popup, Polyline } from 'react-leaflet';
import ImageModal from './ImageModal';
import { useNavigate } from 'react-router-dom';

const TripMyCard = ({ trip, fetchTrips }) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [images, setImages] = useState([]);
  const [comments, setComments] = useState(trip.comments || []);
  const [newComment, setNewComment] = useState('');
  const navigate = useNavigate();

  const points = trip.points.sort((a, b) => a.order - b.order);
  const positions = points.map(point => [point.latitude, point.longitude]);

  const handleDelete = async () => {
    const response = await fetch(`https://localhost:7213/Trip/delete?id=${trip.id}`, {
      method: 'DELETE',
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('jwtToken')}`,
      },
    });

    if (response.ok) {
        fetchTrips();
    }
  };

  const handleCommentChange = (e) => setNewComment(e.target.value);

  const handleAddComment = async () => {
    const data = {
      value: newComment,
      tripId: trip.id
    } 
    const response = await fetch("https://localhost:7213/Comment/add-comment", {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('jwtToken')}`,
      },
      body: JSON.stringify(data)
    });

    if (response.ok) {
      const comment = await response.json();
      const newCommentObj = { userLogin: comment.userLogin, value: comment.value };
      setComments([...comments, newCommentObj]);
      setNewComment('');
    }
  };

  const handleStartEndTrip = async () => {
    var url = !trip.isLaunched
        ? `https://localhost:7213/Trip/start-trip`
        : `https://localhost:7213/Trip/end-trip`;

    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('jwtToken')}`,
      },
      body: JSON.stringify(trip.id)
    });

    if (response.ok) {
        fetchTrips();
    }
  };

  const handleEditClick = () => {
    navigate('/edit', { state: {tripToEdit: trip }});
  };

  const openModal = () => {
    setImages(trip.images);
    setIsModalOpen(true);
  };

  return (
    <div className="border rounded-lg p-4 mb-4 shadow">
      <h2 className="text-xl font-semibold">{trip.name}</h2>
      <p>{trip.description}</p>
      <p>Expected Start: {new Date(trip.expectedStartTime).toLocaleString()}</p>
      <p>Expected End: {new Date(trip.expectedFinishTime).toLocaleString()}</p>
      <button
        onClick={handleStartEndTrip}
        className={`mt-2 bg-blue-500 text-white py-1 px-3 rounded hover:bg-blue-600`}
      >
        {trip.isLaunched ? 'End Trip' : 'Start Trip'}
      </button>
      <button onClick={openModal} className="ml-2 text-blue-500">View Images</button>
      <button onClick={handleDelete} className="ml-2 text-red-500">Delete</button>
      <button onClick={handleEditClick} className="ml-2 text-green-500">Edit</button>

      <div className="my-4" style={{ height: '300px', position: 'relative' }}>
        <MapContainer
            center={[trip.points[0]?.latitude, trip.points[0]?.longitude]}
            zoom={13}
            style={{ height: '100%', width: '100%', zIndex: 0  }}>
            <TileLayer
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />            
            <Polyline positions={positions} color="blue" />
            {points.map((point, index) => (
            <Marker key={index} position={[point.latitude, point.longitude]}>
                <Popup>{`Point ${point.order}`}</Popup>
            </Marker>
            ))}
        </MapContainer>
      </div>

      <ImageModal isModalOpen={isModalOpen} closeModal={() => setIsModalOpen(false)} images={images} />

      <div className="mt-4">
        <h3 className="text-lg font-semibold">Comments</h3>
        {comments.length > 0 ? (
          <ul className="mt-2">
            {comments.map((comment) => (
              <li key={comment.id} className="border-b py-2">
                <p className="font-bold">{comment.userLogin}</p>
                <p>{comment.value}</p>
              </li>
            ))}
          </ul>
        ) : (
          <p>No comments yet.</p>
        )}
      </div>

      <div className="mt-4">
        <h3 className="text-lg font-semibold">Add a Comment</h3>
        <textarea
          value={newComment}
          onChange={handleCommentChange}
          className="w-full p-2 border border-gray-300 rounded mb-2"
          placeholder="Write your comment here"
        />
        <button
          onClick={handleAddComment}
          className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
          disabled={!newComment.trim()}
        >
          Add Comment
        </button>
      </div>
    </div>
  );
};

export default TripMyCard;
