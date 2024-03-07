//var ghostEditor = {
//    bindEvents: function () {
//        this.bindDesignModeToggle();
//        this.bindToolbarButtons();
//    },

//    bindDesignModeToggle: function () {
//        $('#page-content').on('click', function (e) {
//            document.designMode = 'on';
//        });

//        $('#page-content').on('click', function (e) {
//            var $target = $(e.target);

//            if ($target.is('#page-content')) {
//                document.designMode = 'off';
//            }
//        });
//    },

//    bindToolbarButtons: function () {
//        $('#toolbar').on('mousedown', '.icon', function (e) {
//            e.preventDefault();
//            var btnId = $(e.target).attr('id');
//            this.editStyle(btnId);
//        }.bind(this));
//    },

//    editStyle: function (btnId) {
//        var value = null;

//        if (btnId === 'createLink') {
//            if (this.isSelection()) value = prompt('Enter a link:');
//        }

//        document.execCommand(btnId, true, value);
//    },

//    isSelection: function () {
//        var selection = window.getSelection();
//        return selection.anchorOffset !== selection.focusOffset
//    },

//    init: function () {
//        this.bindEvents();
//    },

//}

//ghostEditor.init();

//function getContent() {
//    var content = document.getElementById('page').innerHTML;
//    document.getElementById('Description').value = content;
//}



function formatDoc(cmd, value = null) {
	if (value) {
		document.execCommand(cmd, false, value);
	} else {
		document.execCommand(cmd);
	}
}

function addLink() {
	const url = prompt('Insert url');
	formatDoc('createLink', url);
}




const content = document.getElementById('content');

content.addEventListener('mouseenter', function () {
	const a = content.querySelectorAll('a');
	a.forEach(item => {
		item.addEventListener('mouseenter', function () {
			content.setAttribute('contenteditable', false);
			item.target = '_blank';
		})
		item.addEventListener('mouseleave', function () {
			content.setAttribute('contenteditable', true);
		})
	})
})


const showCode = document.getElementById('show-code');
let active = false;

showCode.addEventListener('click', function () {
	showCode.dataset.active = !active;
	active = !active
	if (active) {
		content.textContent = content.innerHTML;
		content.setAttribute('contenteditable', false);
	} else {
		content.innerHTML = content.textContent;
		content.setAttribute('contenteditable', true);
	}
})



const filename = document.getElementById('filename');

function fileHandle(value) {
	if (value === 'new') {
		content.innerHTML = '';
		filename.value = 'untitled';
	} else if (value === 'txt') {
		const blob = new Blob([content.innerText])
		const url = URL.createObjectURL(blob)
		const link = document.createElement('a');
		link.href = url;
		link.download = `${filename.value}.txt`;
		link.click();
	} else if (value === 'pdf') {
		html2pdf(content).save(filename.value);
	}
}

