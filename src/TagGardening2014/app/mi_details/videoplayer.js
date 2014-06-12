//(function ($, window, document) {

//   // My code
//   var currentMediaItemID = $('#MediaItemTitle').attr('itemid');
//   var videoControl = $('#Video');
//   var currentVideo = $('#Video').get(0);
//   var overlay = $('div.Overlay');
//   var controls = $('div.controls');
//   var buttonPlay = $('div.buttonPlay');
//   var sliderControl = $('div.slider');
//   var timeInfo = $('div.timeInfo');
//   var vidDuration;
//   var countdown;
//   var sliderInterval;

//   videoControl.click(function () {
//      togglePlayPause(this);

//   });

//   buttonPlay.click(function () {
//      //var currentVideo = video.get(0);
//      togglePlayPause(currentVideo);
//   });

//   videoControl.mousemove(function () {
//      clearTimeout(countdown);
//      controls.fadeIn();
//      countdown = setTimeout(function () {
//         controls.fadeOut();
//      }, 1000);
//   });

//   videoControl.on('pause', function () {
//      clearInterval(sliderInterval);
//   });

//   videoControl.on("playing", function () {
//      sliderInterval = setInterval(function () {
//         var currentTime = currentVideo.currentTime;
//         sliderControl.slider("option", "value", currentTime);

//         updateTimeInfo(currentTime);
//      }, 100);

//   });

//   controls.mousemove(function () {
//      clearTimeout(countdown);
//   });

//   controls.mouseout(function () {
//      countdown = setTimeout(function () {
//         controls.fadeOut();
//      }, 1000);
//   });

//   $('.position').on("click", function () {

//   });

//   sliderControl.slider({
//      value: 0,
//      step: 0.01,
//      orientation: 'horizontal',
//      range: 'min',
//      animate: true,
//      slide: function (event, ui) {
//         currentVideo.currentTime = ui.value;
//      },

//   });

//   setTimeout(function () {
//      vidDuration = currentVideo.duration;
//      sliderControl.slider("option", "max", vidDuration);
//      updateTimeInfo(0);
//   }, 500);

//   function togglePlayPause(vid) {
//      if (vid.paused) {
//         overlay.fadeTo(100, 0);
//         buttonPlay.removeClass('paused');
//         vid.play();
//      }
//      else {
//         overlay.fadeTo(100, 0.6);
//         buttonPlay.addClass('paused');
//         vid.pause();
//      }
//   }

//   function updateTimeInfo(currentTime) {
//      timeInfo.text(convertToTimeString(currentTime) + ' / ' + convertToTimeString(vidDuration));
//   }

//   function convertToTimeString(timeNumber) {
//      var restTime = Math.round(timeNumber, 2);
//      var minutes = 0;
//      while (restTime > 60) {
//         restTime -= 60;
//         minutes++;
//      }
//      var seconds = restTime;
//      var minutesText = minutes < 10 ? '0' + minutes : minutes;
//      var secondsText = seconds < 10 ? '0' + seconds : seconds;
//      return minutesText + ':' + secondsText;
//   }

//   $('#AddTag').submit(function (event) {
//      //var tagValue = $('#MediaTagValue').val();
//      //var tagTypeID = $('#MediaTagType').val();
//      //var dataa = { MediaItemID: currentMediaItemID, TagValue: tagValue, TagTypeID: tagTypeID };
//      //if (1 == 1) {
//      //   $.ajax({
//      //      url: '../AddTag/',
//      //      type: 'post',
//      //      data: dataa.serialize(),
//      //      dataType: 'json',
//      //      success: function() {
//      //         var newValueCell = $('<td>' + tagValue + '</td>');
//      //         var newTypeCell = $('<td>' + tagTypeID + '</td>');
//      //         var newRow = $('<tr/>').append(newValueCell).append(newTypeCell);
//      //         $('#TagList tr:last-child').remove();
//      //         $('#TagList').append(newRow);
//      //      },
//      //      error: function(xhr, status, error) {
//      //         //display error
//      //      }
//      //   });
//      //} else {
//      //   //tell user what a valid email address looks like
//      //}
//      //return false;
//      //// post the data to the controller
//      var newMediaTag = new NewMediaTag();
//      newMediaTag.MediaItemID = currentMediaItemID;
//      newMediaTag.Value = $('#MediaTagValue').val();
//      newMediaTag.MediaTagTypeID = $('#MediaTagType').val();
//      var postData = JSON.stringify(newMediaTag);
//      var requrl = $(this).data('request-url');
//      $.ajax({
//         url: requrl,
//         type: 'post',
//         contentType: 'application/json; charset=utf-8',
//         data: postData,
//         success: function (content) {
//            //var newValueCell = $('<td>' + newMediaTag.Value + '</td>');
//            //var newTypeCell = $('<td>' + newMediaTag.MediaTagTypeID + '</td>');
//            //var newRow = $('<tr/>').append(newValueCell).append(newTypeCell);
//            $('#TagList').html(content);
//         }
//      });
//      event.preventDefault();
//   });

//   function processResult() {
//      alert('success');
//   }

//   function NewMediaTag() {
//      this.MediaItemID = 0;
//      this.Value = "";
//      this.MediaTagTypeID = 0;
//   }

//   $('#Video').get(0).SetTime = function (timestampString) {
//      var offset = 0.5;
//      var timestampArray = timestampString.split(":");
//      var newTime = (parseInt(timestampArray[0]) * 3600) + (parseInt(timestampArray[1]) * 60) + (parseInt(timestampArray[2])) + (parseInt(timestampArray[3]) / 1000) - offset;
//      sliderControl.slider('option', 'value', newTime);
//      currentVideo.currentTime = newTime;
//      updateTimeInfo(newTime);
//   };

//}(window.jQuery, window, document));