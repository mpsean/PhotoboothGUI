@echo off
REM Create a virtual environment
python -m venv .venv

REM Activate the virtual environment
call .venv\Scripts\activate

REM Install the dependencies from requirements.txt
pip install -r requirements.txt

REM Pause to keep the terminal open
pause
