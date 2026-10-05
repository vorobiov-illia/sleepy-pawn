import { dotnet } from './_framework/dotnet.js'

const { setModuleImports, getAssemblyExports, getConfig, runMain } = await dotnet
    .create();

const config = getConfig();
const exports = await getAssemblyExports(config.mainAssemblyName);

function update() {
    updateBoard();
    document.getElementById('state').innerText = exports.SleepyPawnWeb.DebugGameState();
}
function updateBoard() {
    const boardText = exports.SleepyPawnWeb.DebugBoard();
    document.getElementById('board').innerText = boardText;
}

function submitMove() {
    const inputField = document.getElementById('moveinput');
    if (exports.SleepyPawnWeb.TryMove(inputField.value)) {
        document.getElementById('error').style.display = 'none';
    }
    else {
        document.getElementById('error').style.display = 'block';
    }
    inputField.value = '';
    update();
}

document.getElementById('reset').addEventListener('click', e => {
    exports.SleepyPawnWeb.Reset();
    document.getElementById('error').style.display = 'none';
    e.preventDefault();
    update();
});

document.getElementById('trymove').addEventListener('click', e => {
    submitMove();
    e.preventDefault();
});

document.getElementById('moveinput').addEventListener('keydown', e => {
    if (e.key === 'Enter') {
        e.preventDefault();
        submitMove();
    }
});

await runMain();
update();