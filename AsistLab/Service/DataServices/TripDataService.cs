using System.Runtime.InteropServices.Marshalling;
using AutoMapper;
using Common.Domains;
using Common.Dtos;
using Hangfire;
using Repository.Repositories.Interfaces;

namespace Service.DataServices;

public class TripDataService
{
    private readonly ITripRepository _tripRepository;
    private readonly ImageDataService _imageDataService;
    private readonly IMapper _mapper;

    public TripDataService(
        ITripRepository tripRepository,
        ImageDataService imageDataService,
        IMapper mapper)
    {
        _tripRepository = tripRepository;
        _imageDataService = imageDataService;
        _mapper = mapper;
    }

    public async Task PutTripAsync(EditTripDto dto)
    {
        var model = _mapper.Map<Trip>(dto);
        var images = await _imageDataService.SaveImagesToLocalStorageAsync(dto.Images);
        model.Images = images;
        
        if (model.Id == 0)
            await _tripRepository.AddAsync(model);
        else
             await _tripRepository.UpdateAll(model);

        BackgroundJob.Schedule(() => UpdateStatus(model.Id), dto.ExpectedFinishTime);
    }
    
    public async Task<List<TripDto>> GetMyAsync(int userId)
    {
        var model = await _tripRepository.GetMyAsync(userId);
        var dtos = _mapper.Map<List<TripDto>>(model);
        return dtos;
    }
    
    public async Task<List<TripDto>> GetHistoryAsync(int userId)
    {
        var model = await _tripRepository.GetMyHistoryAsync(userId);
        var dtos = _mapper.Map<List<TripDto>>(model);
        return dtos;
    }
    
    public async Task<List<TripDto>> GetFriendTripsAsync(int userId)
    {
        var model = await _tripRepository.GetFriendTripsAsync(userId);
        var dtos = _mapper.Map<List<TripDto>>(model);
        return dtos;
    }
    
    public async Task Delete(int id)
    {
        await _tripRepository.DeleteAsync(new Trip {Id = id});
    }
    
    public async Task StartTrip(int tripId)
    {
        var model = await _tripRepository.GetByIdAsync(tripId);
        model.RealStartTime = DateTime.UtcNow;
        model.IsLaunched = true;
        await _tripRepository.UpdateAsync(model);
    }
    
    public async Task EndTrip(int tripId)
    {
        var model = await _tripRepository.GetByIdAsync(tripId);
        model.RealFinishTime = DateTime.UtcNow;
        model.IsLaunched = false;
        model.IsFinish = true;
        await _tripRepository.UpdateAsync(model);
    }
    
    public async Task UpdateStatus(int tripId)
    {
        var model = await _tripRepository.GetByIdAsync(tripId);
        if (model != null)
        {
            model!.IsFinish = true;
            model.RealStartTime = model.ExpectedStartTime;
            model.RealFinishTime = model.ExpectedFinishTime;
            await _tripRepository.UpdateAsync(model);
        }
    }
}