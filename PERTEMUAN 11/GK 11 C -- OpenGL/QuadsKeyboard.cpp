#include <windows.h>
#include <gl/Gl.h>
#include <gl/Glu.h>
#include "glut.h"

int x1, x2, y1, y2 = 0;

void display(void)
{
    glClear ( GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT );
    glPushMatrix ( );

    // quADS
    glBegin(GL_QUADS);
    glColor3f(1.0, 0.0, 0.0);
    glVertex2i(-20.0,20.0);
    glColor3f(0.0, 0.0, 1.0);
    glVertex2i(20.0,20.0);
    glColor3f(0.0, 1.0, 1.0);
    glVertex2i(20.0,-20.0);
    glColor3f(1.0, 1.0, 0.0);
    glVertex2i(-20.0,-20.0);
    glEnd();

    glPopMatrix  ( );
    glutSwapBuffers ( );
}

void init ( void )   //clear graphic device
{
    glEnable     ( GL_DEPTH_TEST );
    glClearColor ( 0.0, 0.0, 0.0, 0.0 );
    glShadeModel (GL_FLAT); // hanya satu warna saja fillnya
}

void reshape(int w, int h)
{
 glViewport (0, 0, (GLsizei) w, (GLsizei) h);
 glMatrixMode(GL_PROJECTION);
 glLoadIdentity();

 // Perhitugnan skala jadinya diatur openGL (fitur jadi)
 glOrtho(-50.0, 50.0, -50.0, 50.0, -1.0, 1.0); // GK 11 N

 glMatrixMode(GL_MODELVIEW);
 glLoadIdentity();
}

#pragma argsused
void keyboard ( unsigned char key, int x1, int y1, int x2, int y2 )
{
    /*
    switch ( key ) {
    case 27:  /*  Escape key
        exit ( 0 );
        break;
    case 'f': case 'F':
        glutFullScreen ( );
        break;
    case 'w': case 'W':
        glutReshapeWindow ( 250,250 );
        break;
    } */

    switch (key) {
        case 'A': case 'a': x1--; x2--; break;
        case 'S': case 's': y1++; y2++; break;
        case 'W': case 'w': y1--; y2--; break;
        case 'D': case 'd': x1++; x2++; break;
    }
}

void mouse(int button, int state, int x, int y) // seperti event MouseClick
{
    switch (button) {
    case GLUT_LEFT_BUTTON:
        x1--; x2--;
        break;
    case GLUT_MIDDLE_BUTTON:

        break;
    case GLUT_RIGHT_BUTTON:
        break;
    default:;
    }

    display();
}

int main ( int argc, char** argv )
{
    glutInit ( &argc, argv );            //Initialize window Title
    glutInitDisplayMode ( GLUT_RGB | GLUT_DOUBLE );  //Initial display type 24 bit RGB and double buffer
    glutInitWindowSize  ( 250, 250 );    //Initialize window resolution
    glutCreateWindow    ( argv[0]  );    //Create an animation window
    init ( );                            //clear graphic device
    glutReshapeFunc     ( reshape  );    //Initialize refresh frame function
    glutKeyboardFunc    ( keyboard );    //Initialize keyboard response function
    glutMouseFunc       ( mouse    );    //Initialize mouse response function
    glutDisplayFunc     ( display  );    //Initialize the animation function
    glutMainLoop        ( );             //Looping the animation
    return 0;

    /*
        exit(1) mirip dengan return 1;
        exit(0) mirip dengan return 0;

        keyboard fucntion mesti ada
        untuk ngasih tau openGL 

        OPENGL SEBENARNYA BELUM TAU
        untuk ngasih tau, masukkan dalam arguments dalam fungsi dalam main
    */
}