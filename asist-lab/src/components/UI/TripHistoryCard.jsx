import React, { useState } from 'react';
import { MapContainer, TileLayer, Marker, Popup, Polyline } from 'react-leaflet';
import ImageModal from './ImageModal';

const TripHistoryCard = ({ trip, fetchTrips }) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [comments, setComments] = useState(trip.comments || []);
  const [newComment, setNewComment] = useState('');
  const [images, setImages] = useState([]);

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

  const openModal = () => {
    setImages(trip.images);
    setIsModalOpen(true);
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

  return (
    <div className="border rounded-lg p-4 mb-4 shadow">
      <h2 className="text-xl font-semibold">{trip.name}</h2>
      <p>{trip.description}</p>
      <p>Expected Start: {new Date(trip.expectedStartTime).toLocaleString()}</p>
      <p>Expected End: {new Date(trip.expectedFinishTime).toLocaleString()}</p>
      <p>Real Start: {new Date(trip.realStartTime).toLocaleString()}</p>
      <p>Real End: {new Date(trip.realFinishTime).toLocaleString()}</p>
      <p>Duration: {trip.duration}</p>
      <button onClick={openModal} className="ml-2 text-blue-500">View Images</button>
      <button onClick={handleDelete} className="ml-2 text-red-500">Delete</button>

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

export default TripHistoryCard;
